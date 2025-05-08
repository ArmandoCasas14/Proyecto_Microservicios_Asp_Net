namespace Proyecto_Microservicios_Asp_Net.Models
{
    public class LogUsers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Usuario? Usuario { get; set; }
        public string nickname { get; set; }
    }
}
