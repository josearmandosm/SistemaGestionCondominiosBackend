using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.DTOs.Visita
{
    public class VisitaDto
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaVisita { get; set; }
        public TipoVisitaEnum TipoVisita { get; set; }
        //public int IdUsuario { get; set; }
    }
}
