namespace SistemaGestionCondominios.DTOs.Documento
{
    public class DocumentoPutDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string RutaArchivo { get; set; }
    }
}
