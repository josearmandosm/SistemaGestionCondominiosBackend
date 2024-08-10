using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Residencia;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ResidenciasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Residencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResidenciaDto>>> GetResidencia()
        {
            var residencias = await _context.Residencias.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ResidenciaDto>>(residencias));
        }

        // GET: api/Residencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResidenciaDto>> GetResidencia(int id)
        {
            var residencia = await _context.Residencias.FindAsync(id);

            if (residencia == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ResidenciaDto>(residencia));
        }

        // PUT: api/Residencias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResidencia(int id, ResidenciaPutDto residenciaDto)
        {
            if (id != residenciaDto.Id)
            {
                return BadRequest();
            }

            var residencia = await _context.Residencias.FindAsync(id);
            
            if (residencia == null)
            {
                return NotFound();
            }

            residencia.Numero = residenciaDto.Numero;
            residencia.Direccion = residenciaDto.Direccion;

            _context.Entry(residencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidenciaExists(id))
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

        // POST: api/Residencias
        [HttpPost]
        public async Task<ActionResult<Residencia>> PostResidencia(ResidenciaPostDto residenciaDto)
        {
            var residencia = _mapper.Map<Residencia>(residenciaDto);
            _context.Residencias.Add(residencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResidencia", new { id = residencia.Id }, residencia);
        }

        // DELETE: api/Residencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResidencia(int id)
        {
            var residencia = await _context.Residencias.FindAsync(id);
            if (residencia == null)
            {
                return NotFound();
            }

            _context.Residencias.Remove(residencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResidenciaExists(int id)
        {
            return _context.Residencias.Any(e => e.Id == id);
        }
    }
}
