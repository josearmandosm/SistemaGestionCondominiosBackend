using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Encuesta;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public EncuestasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Encuestas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncuestaDto>>> GetEncuesta()
        {
            var Encuestas = await _context.Encuestas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EncuestaDto>>(Encuestas));
        }

        // GET: api/Encuestas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncuestaDto>> GetEncuesta(int id)
        {
            var Encuesta = await _context.Encuestas.FindAsync(id);

            if (Encuesta == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EncuestaDto>(Encuesta));
        }

        // PUT: api/Encuestas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncuesta(int id, EncuestaPutDto EncuestaDto)
        {
            if (id != EncuestaDto.Id)
            {
                return BadRequest();
            }

            var Encuesta = await _context.Encuestas.FindAsync(id);
            
            if (Encuesta == null)
            {
                return NotFound();
            }

            Encuesta.Titulo = EncuestaDto.Titulo;
            Encuesta.Descripcion = EncuestaDto.Descripcion;

            _context.Entry(Encuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncuestaExists(id))
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

        // POST: api/Encuestas
        [HttpPost]
        public async Task<ActionResult<Encuesta>> PostEncuesta(EncuestaPostDto EncuestaDto)
        {
            var Encuesta = _mapper.Map<Encuesta>(EncuestaDto);
            _context.Encuestas.Add(Encuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEncuesta", new { id = Encuesta.Id }, Encuesta);
        }

        // DELETE: api/Encuestas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncuesta(int id)
        {
            var Encuesta = await _context.Encuestas.FindAsync(id);
            if (Encuesta == null)
            {
                return NotFound();
            }

            _context.Encuestas.Remove(Encuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EncuestaExists(int id)
        {
            return _context.Encuestas.Any(e => e.Id == id);
        }
    }
}
