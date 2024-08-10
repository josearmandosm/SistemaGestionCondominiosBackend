using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.DTOs.Mantenimiento
{
    public class MantenimientoPostDto
    {
        public string Descripcion { get; set; }
        public EstadoMantenimientoEnum Estado { get; set; }
        public DateTime FechaCompletado { get; set; }
        //public int UsuarioId { get; set; }
    }
}
