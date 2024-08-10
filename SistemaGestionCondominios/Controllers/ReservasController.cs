using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Reserva;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaDto>>> GetReserva()
        {
            var Reservas = await _context.Reservas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ReservaDto>>(Reservas));
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaDto>> GetReserva(int id)
        {
            var Reserva = await _context.Reservas.FindAsync(id);

            if (Reserva == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReservaDto>(Reserva));
        }

        // PUT: api/Reservas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, ReservaPutDto ReservaDto)
        {
            if (id != ReservaDto.Id)
            {
                return BadRequest();
            }

            var Reserva = await _context.Reservas.FindAsync(id);
            
            if (Reserva == null)
            {
                return NotFound();
            }

            Reserva.AreaComun = ReservaDto.AreaComun;
            Reserva.Nombre = ReservaDto.Nombre;
            Reserva.Cedula = ReservaDto.Cedula;
            Reserva.FechaEvento = ReservaDto.FechaEvento;
            Reserva.Estado = ReservaDto.Estado;

            _context.Entry(Reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
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

        // POST: api/Reservas
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(ReservaPostDto ReservaDto)
        {
            var Reserva = _mapper.Map<Reserva>(ReservaDto);
            _context.Reservas.Add(Reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = Reserva.Id }, Reserva);
        }

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var Reserva = await _context.Reservas.FindAsync(id);
            if (Reserva == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(Reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
