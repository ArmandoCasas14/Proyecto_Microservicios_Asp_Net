namespace Proyecto_Microservicios_Asp_Net.Models
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }
    }
}
