using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Visita;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public VisitasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Visitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitaDto>>> GetVisita()
        {
            var Visitas = await _context.Visitas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VisitaDto>>(Visitas));
        }

        // GET: api/Visitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitaDto>> GetVisita(int id)
        {
            var Visita = await _context.Visitas.FindAsync(id);

            if (Visita == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VisitaDto>(Visita));
        }

        // PUT: api/Visitas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisita(int id, VisitaPutDto VisitaDto)
        {
            if (id != VisitaDto.Id)
            {
                return BadRequest();
            }

            var Visita = await _context.Visitas.FindAsync(id);
            
            if (Visita == null)
            {
                return NotFound();
            }

            Visita.Cedula = VisitaDto.Cedula;
            Visita.Nombre = VisitaDto.Nombre;
            Visita.TipoVisita = VisitaDto.TipoVisita;

            _context.Entry(Visita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitaExists(id))
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

        // POST: api/Visitas
        [HttpPost]
        public async Task<ActionResult<Visita>> PostVisita(VisitaPostDto VisitaDto)
        {
            var Visita = _mapper.Map<Visita>(VisitaDto);
            _context.Visitas.Add(Visita);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisita", new { id = Visita.Id }, Visita);
        }

        // DELETE: api/Visitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisita(int id)
        {
            var Visita = await _context.Visitas.FindAsync(id);
            if (Visita == null)
            {
                return NotFound();
            }

            _context.Visitas.Remove(Visita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitaExists(int id)
        {
            return _context.Visitas.Any(e => e.Id == id);
        }
    }
}
