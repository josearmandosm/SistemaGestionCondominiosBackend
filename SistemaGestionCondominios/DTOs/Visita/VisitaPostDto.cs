using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.DTOs.Visita
{
    public class VisitaPostDto
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public TipoVisitaEnum TipoVisita { get; set; }
        //public int IdUsuario { get; set; }
    }
}
