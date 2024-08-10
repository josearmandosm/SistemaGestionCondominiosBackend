﻿using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.DTOs.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public RolEnum Rol { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
