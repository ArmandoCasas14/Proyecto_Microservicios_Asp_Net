using Microsoft.EntityFrameworkCore;
using Proyecto_Microservicios_Asp_Net.Models;
namespace Proyecto_Microservicios_Asp_Net.Datos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Insertar valores predefinidos en la tabla Estado
            modelBuilder.Entity<Estado>().HasData(
                new Estado { Id = 1, descripcion = "Activo" },
                new Estado { Id = 2, descripcion = "Inactivo" },
                new Estado { Id = 3, descripcion = "Bloqueado" }
            );
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, descripcion = "Administrador" },
                new Rol { Id = 2, descripcion = "Cliente" }
            );
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, nombre = "Jorge", apellido = "Isaza", email = "admin@example.com", direccion = "Calle 123", telefono = "123456789" },
                new Usuario { Id = 2, nombre = "Juan", apellido = "Pérez", email = "juan@example.com", direccion = "Avenida 456", telefono = "987654321" }
            );
            modelBuilder.Entity<Login>().HasData(
                new Login { Id = 1, UsuarioId = 1, Nickname = "Admin123", Password = "Admin@123", EstadoId = 1, RolId = 1 },
                new Login { Id = 2, UsuarioId = 2, Nickname = "User456", Password = "User@456", EstadoId = 1, RolId = 2 }
            );
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Electrónica" },
                new Categoria { Id = 2, Nombre = "Accesorios" }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, nombre = "Laptop", descripcion = "Laptop de alto rendimiento", precio = "1200", stock = 10, CategoriaId = 1 },
                new Producto { Id = 2, nombre = "Mouse", descripcion = "Mouse inalámbrico", precio = "25", stock = 50, CategoriaId = 2 }
            );
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<LogManualUsers> LogManualUsers { get; set; }

    }
}
