using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.DTOs.Pago
{
    public class PagoPostDto
    {
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionPago { get; set; }
        public TipoPagoEnum TipoPago { get; set; }
        public EstadoPagoEnum Estado { get; set; }
        //public int UsuarioId { get; set; }
    }
}
