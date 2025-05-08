namespace Proyecto_Microservicios_Asp_Net.Models
{
    public class LogCategory
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }   
        public string nombre { get; set;}
    }
}
