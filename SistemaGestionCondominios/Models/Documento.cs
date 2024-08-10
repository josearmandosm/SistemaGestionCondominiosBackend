namespace SistemaGestionCondominios.Models
{
    public class Documento
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSubida { get; set; }
        public string RutaArchivo { get; set; }
    }
}
