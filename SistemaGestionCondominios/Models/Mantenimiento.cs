namespace SistemaGestionCondominios.Models
{
    public class Mantenimiento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public EstadoMantenimientoEnum Estado { get; set; }
        public DateTime FechaCompletado { get; set; }
        //public int UsuarioId { get; set; }
    }
}
