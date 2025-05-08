using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Microservicios_Asp_Net.Datos;
using Proyecto_Microservicios_Asp_Net.Models;

namespace Proyecto_Microservicios_Asp_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductoController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Producto
        //listar productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.nombre,
                    Descripcion = p.descripcion,
                    Precio = p.precio,
                    Stock = p.stock,
                    Categoria = p.Categoria.Nombre
                })
                    .ToListAsync();
            return Ok(productos);
        }
        // GET: api/Producto/2
        // obtener  producto especifico
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos
           .Include(p => p.Categoria)
           .Where(p => p.Id == id)
           .Select(p => new ProductoDto
           {
               Id = p.Id,
               Nombre = p.nombre,
               Descripcion = p.descripcion,
               Precio = p.precio,
               Stock = p.stock,
               Categoria = p.Categoria.Nombre
           })
           .FirstOrDefaultAsync();

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }
        // POST: api/Producto
        //guardar productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }
        // PUT: api/Producto/1
        //actualizar producto
        //ejemplo 
        // {
        // "id": 1,
        // "nombre": "Laptop asus",
        // "descripcion": "Laptop alto rendimiendo",
        // "precio": "1200",
        // "stock": 11,
        //"categoria": "Electrónica"
        // }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            // esto verifica si el id de la url api/Producto/1 es igual al que se pone en json en el frontend es necesario para seguridad //
            if (id != producto.Id)
            {
                return BadRequest("El ID proporcionado no coincide con el ID del producto.");
            }

            // Verificar si el producto existe en la base de datos
            var productoExistente = await _context.Productos.FindAsync(id);
            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Actualizar los campos del producto existente
            productoExistente.nombre = producto.nombre;
            productoExistente.descripcion = producto.descripcion;
            productoExistente.precio = producto.precio;
            productoExistente.stock = producto.stock;
            productoExistente.CategoriaId = producto.CategoriaId;

            try
            {
                // Guardar los cambios
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Hubo un error al actualizar el producto.");
            }

            return NoContent();
        }

        // DELETE: api/Producto/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound("no encontrado");
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
        // GET /api/producto/por-categoria/1
        [HttpGet("por-categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductosPorCategoria(int categoriaId)
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaId)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.nombre,
                    Descripcion = p.descripcion,
                    Precio = p.precio,
                    Stock = p.stock,
                    Categoria = p.Categoria.Nombre
                })
                .ToListAsync();
            

            return Ok(productos);
        }
        //GET /api/producto/buscar/gamer esto busca en el nombre y en la descripcion
        [HttpGet("buscar/{termino}")]
        public async Task<ActionResult<IEnumerable<Producto>>> BuscarPorNombre(string termino)
        {
            var productos = await _context.Productos
                .Where(p => p.nombre.ToLower().Contains(termino.ToLower()) ||
                p.descripcion.ToLower().Contains(termino.ToLower()))
                .Include(p => p.Categoria)
                .ToListAsync();
        

            if (productos == null || productos.Count == 0)
            {
                return NotFound("No se encontraron productos con ese nombre.");
            }

            return Ok(productos);
        }
    }

}
