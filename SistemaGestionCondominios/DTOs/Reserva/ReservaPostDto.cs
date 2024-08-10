﻿using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.DTOs.Reserva
{
    public class ReservaPostDto
    {
        public string AreaComun { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public DateTime FechaEvento { get; set; }
        public EstadoReservaEnum Estado { get; set; }
        //public int UsuarioId { get; set; }
    }
}
