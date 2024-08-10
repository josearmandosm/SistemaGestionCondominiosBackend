using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.RespuestaEncuesta;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaEncuestasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public RespuestaEncuestasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/RespuestaEncuestas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RespuestaEncuestaDto>>> GetRespuestaEncuesta()
        {
            var RespuestaEncuestas = await _context.RespuestaEncuestas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<RespuestaEncuestaDto>>(RespuestaEncuestas));
        }

        // GET: api/RespuestaEncuestas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaEncuestaDto>> GetRespuestaEncuesta(int id)
        {
            var RespuestaEncuesta = await _context.RespuestaEncuestas.FindAsync(id);

            if (RespuestaEncuesta == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RespuestaEncuestaDto>(RespuestaEncuesta));
        }

        // PUT: api/RespuestaEncuestas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRespuestaEncuesta(int id, RespuestaEncuestaPutDto RespuestaEncuestaDto)
        {
            if (id != RespuestaEncuestaDto.Id)
            {
                return BadRequest();
            }

            var RespuestaEncuesta = await _context.RespuestaEncuestas.FindAsync(id);
            
            if (RespuestaEncuesta == null)
            {
                return NotFound();
            }

            RespuestaEncuesta.Respuesta = RespuestaEncuestaDto.Respuesta;
            RespuestaEncuesta.EncuestaId = RespuestaEncuestaDto.EncuestaId;

            _context.Entry(RespuestaEncuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespuestaEncuestaExists(id))
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

        // POST: api/RespuestaEncuestas
        [HttpPost]
        public async Task<ActionResult<RespuestaEncuesta>> PostRespuestaEncuesta(RespuestaEncuestaPostDto RespuestaEncuestaDto)
        {
            var RespuestaEncuesta = _mapper.Map<RespuestaEncuesta>(RespuestaEncuestaDto);
            _context.RespuestaEncuestas.Add(RespuestaEncuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRespuestaEncuesta", new { id = RespuestaEncuesta.Id }, RespuestaEncuesta);
        }

        // DELETE: api/RespuestaEncuestas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRespuestaEncuesta(int id)
        {
            var RespuestaEncuesta = await _context.RespuestaEncuestas.FindAsync(id);
            if (RespuestaEncuesta == null)
            {
                return NotFound();
            }

            _context.RespuestaEncuestas.Remove(RespuestaEncuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RespuestaEncuestaExists(int id)
        {
            return _context.RespuestaEncuestas.Any(e => e.Id == id);
        }
    }
}
