using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Microservicios_Asp_Net.Datos;
using Proyecto_Microservicios_Asp_Net.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyecto_Microservicios_Asp_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public LoginController(AppDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Logins
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Nickname == loginDto.Username);

            if (user == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            if (user.Password != loginDto.Password)
            {
                return Unauthorized("Contraseña incorrecta.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Nickname),
                    new Claim(ClaimTypes.Role, user.Rol?.descripcion ?? "Usuario")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],      
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var logEntry = new LogManualUsers
            {
                UsuarioId = user.UsuarioId,
                LoginDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.LogManualUsers.Add(logEntry);
            await _context.SaveChangesAsync();


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });

        }

        [HttpGet("logged-users")]
        public async Task<IActionResult> GetLoggedUsers()
        {
            var loggedUsers = await _context.LogManualUsers
       .Where(l => l.IsActive)
       .Join(_context.Usuarios,
           log => log.UsuarioId,
           user => user.Id,
           (log, user) => new
           {
               log.Id,
               log.LoginDate,
               user.nombre,
               user.apellido,
               user.email,
               user.telefono
           })
       .ToListAsync();

            return Ok(loggedUsers);

        }

    }
}
