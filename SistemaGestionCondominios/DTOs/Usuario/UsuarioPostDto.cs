using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.DTOs.Usuario
{
    public class UsuarioPostDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RolEnum Rol { get; set; }
    }
}
