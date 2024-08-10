namespace SistemaGestionCondominios.DTOs.Notificacion
{
    public class NotificacionPutDto
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public bool Leido { get; set; }
        //public int UsuarioId { get; set; }
    }
}
