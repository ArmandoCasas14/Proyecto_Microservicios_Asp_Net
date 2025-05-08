namespace Proyecto_Microservicios_Asp_Net.Models
{
    public class LogManualUsers
    {
        
            
            public int Id { get; set; }  
            public int UsuarioId { get; set; }
            public Usuario? Usuario { get; set; }
            public DateTime LoginDate { get; set; }  
            public bool IsActive { get; set; }
        
    }
}
