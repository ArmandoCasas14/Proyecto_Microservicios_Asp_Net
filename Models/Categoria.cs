using System.ComponentModel.DataAnnotations;

namespace Proyecto_Microservicios_Asp_Net.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        
    }
}
