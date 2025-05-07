namespace Proyecto_Microservicios_Asp_Net.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string descripcion {  get; set; }
        public string precio { get; set;}
        public int stock { get; set;}
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
