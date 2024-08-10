using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Notificacion;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public NotificacionesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Notificaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacionDto>>> GetNotificacion()
        {
            var Notificaciones = await _context.Notificaciones.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<NotificacionDto>>(Notificaciones));
        }

        // GET: api/Notificaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificacionDto>> GetNotificacion(int id)
        {
            var Notificacion = await _context.Notificaciones.FindAsync(id);

            if (Notificacion == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NotificacionDto>(Notificacion));
        }

        // PUT: api/Notificaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificacion(int id, NotificacionPutDto NotificacionDto)
        {
            if (id != NotificacionDto.Id)
            {
                return BadRequest();
            }

            var Notificacion = await _context.Notificaciones.FindAsync(id);
            
            if (Notificacion == null)
            {
                return NotFound();
            }

            Notificacion.Mensaje = NotificacionDto.Mensaje;
            Notificacion.Leido = NotificacionDto.Leido;

            _context.Entry(Notificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notificaciones
        [HttpPost]
        public async Task<ActionResult<Notificacion>> PostNotificacion(NotificacionPostDto NotificacionDto)
        {
            var Notificacion = _mapper.Map<Notificacion>(NotificacionDto);
            _context.Notificaciones.Add(Notificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificacion", new { id = Notificacion.Id }, Notificacion);
        }

        // DELETE: api/Notificaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            var Notificacion = await _context.Notificaciones.FindAsync(id);
            if (Notificacion == null)
            {
                return NotFound();
            }

            _context.Notificaciones.Remove(Notificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificacionExists(int id)
        {
            return _context.Notificaciones.Any(e => e.Id == id);
        }
    }
}
