using System.ComponentModel.DataAnnotations;
namespace Proyecto_Microservicios_Asp_Net.Models
{
        public class Estado
        {
            public int Id { get; set; }
            [Required]
            [StringLength(20)]
            public string descripcion { get; set; }
        }
    
}
