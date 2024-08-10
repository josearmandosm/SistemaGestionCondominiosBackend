namespace SistemaGestionCondominios.DTOs.RespuestaEncuesta
{
    public class RespuestaEncuestaPutDto
    {
        public int Id { get; set; }
        public string Respuesta { get; set; }
        public int EncuestaId { get; set; }
        //public int UsuarioId { get; set; }
    }
}
