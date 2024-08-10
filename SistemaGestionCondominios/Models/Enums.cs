using System.Runtime.Serialization;

namespace SistemaGestionCondominios.Models
{
    public enum RolEnum
    {
        [EnumMember(Value = "Administrador")]
        Administrador,
        [EnumMember(Value = "Residente")]
        Residente,
        [EnumMember(Value = "Visitante")]
        Visitante,
        [EnumMember(Value = "Mantenimiento")]
        Mantenimiento,
        [EnumMember(Value = "Consejo")]
        Consejo
    }

    public enum EstadoPagoEnum
    {
        [EnumMember(Value = "Pendiente")]
        Pendiente,
        [EnumMember(Value = "Completado")]
        Completado
    }

    public enum TipoPagoEnum
    {
        [EnumMember(Value = "Mantenimiento")]
        Mantenimiento,
        [EnumMember(Value = "Extracurricular")]
        Extracurricular,
        [EnumMember(Value = "Otro")]
        Otro
    }

    public enum EstadoMantenimientoEnum
    {
        [EnumMember(Value = "Pendiente")]
        Pendiente,
        [EnumMember(Value = "EnProgreso")]
        EnProgreso,
        [EnumMember(Value = "Completado")]
        Completado
    }

    public enum EstadoReservaEnum
    {
        [EnumMember(Value = "Pendiente")]
        Pendiente,
        [EnumMember(Value = "Aprobada")]
        Aprobada,
        [EnumMember(Value = "Rechazada")]
        Rechazada
    }

    public enum TipoVisitaEnum
    {
        [EnumMember(Value = "Programada")]
        Programada,
        [EnumMember(Value = "Manual")]
        Manual
    }
}
