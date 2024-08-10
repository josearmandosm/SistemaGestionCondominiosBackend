using AutoMapper;
using SistemaGestionCondominios.DTOs.Documento;
using SistemaGestionCondominios.DTOs.Encuesta;
using SistemaGestionCondominios.DTOs.Mantenimiento;
using SistemaGestionCondominios.DTOs.Notificacion;
using SistemaGestionCondominios.DTOs.Pago;
using SistemaGestionCondominios.DTOs.Reserva;
using SistemaGestionCondominios.DTOs.Residencia;
using SistemaGestionCondominios.DTOs.RespuestaEncuesta;
using SistemaGestionCondominios.DTOs.Usuario;
using SistemaGestionCondominios.DTOs.Visita;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping Residencia
            CreateMap<Residencia, ResidenciaDto>().ReverseMap();
            CreateMap<Residencia, ResidenciaPostDto>().ReverseMap();
            CreateMap<Residencia, ResidenciaPutDto>().ReverseMap();

            // Mapping Documento
            CreateMap<Documento, DocumentoDto>().ReverseMap();
            CreateMap<Documento, DocumentoPostDto>().ReverseMap();
            CreateMap<Documento, DocumentoPutDto>().ReverseMap();

            // Mapping Encuesta
            CreateMap<Encuesta, EncuestaDto>().ReverseMap();
            CreateMap<Encuesta, EncuestaPostDto>().ReverseMap();
            CreateMap<Encuesta, EncuestaPutDto>().ReverseMap();

            // Mapping Mantenimiento
            CreateMap<Mantenimiento, MantenimientoDto>().ReverseMap();
            CreateMap<Mantenimiento, MantenimientoPostDto>().ReverseMap();
            CreateMap<Mantenimiento, MantenimientoPutDto>().ReverseMap();

            // Mapping Notificacion
            CreateMap<Notificacion, NotificacionDto>().ReverseMap();
            CreateMap<Notificacion, NotificacionPostDto>().ReverseMap();
            CreateMap<Notificacion, NotificacionPutDto>().ReverseMap();

            // Mapping Pago
            CreateMap<Pago, PagoDto>().ReverseMap();
            CreateMap<Pago, PagoPostDto>().ReverseMap();
            CreateMap<Pago, PagoPutDto>().ReverseMap();

            // Mapping Reserva
            CreateMap<Reserva, ReservaDto>().ReverseMap();
            CreateMap<Reserva, ReservaPostDto>().ReverseMap();
            CreateMap<Reserva, ReservaPutDto>().ReverseMap();

            // Mapping RespuestaEncuesta
            CreateMap<RespuestaEncuesta, RespuestaEncuestaDto>().ReverseMap();
            CreateMap<RespuestaEncuesta, RespuestaEncuestaPostDto>().ReverseMap();
            CreateMap<RespuestaEncuesta, RespuestaEncuestaPutDto>().ReverseMap();

            // Mapping Usuario
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioPostDto>().ReverseMap();
            CreateMap<Usuario, UsuarioPutDto>().ReverseMap();

            // Mapping Visita
            CreateMap<Visita, VisitaDto>().ReverseMap();
            CreateMap<Visita, VisitaPostDto>().ReverseMap();
            CreateMap<Visita, VisitaPutDto>().ReverseMap();

        }
    }
}
