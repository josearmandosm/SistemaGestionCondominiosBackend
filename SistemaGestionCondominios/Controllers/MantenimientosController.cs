using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Mantenimiento;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public MantenimientosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Mantenimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MantenimientoDto>>> GetMantenimiento()
        {
            var Mantenimientos = await _context.Mantenimientos.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<MantenimientoDto>>(Mantenimientos));
        }

        // GET: api/Mantenimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MantenimientoDto>> GetMantenimiento(int id)
        {
            var Mantenimiento = await _context.Mantenimientos.FindAsync(id);

            if (Mantenimiento == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MantenimientoDto>(Mantenimiento));
        }

        // PUT: api/Mantenimientos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMantenimiento(int id, MantenimientoPutDto MantenimientoDto)
        {
            if (id != MantenimientoDto.Id)
            {
                return BadRequest();
            }

            var Mantenimiento = await _context.Mantenimientos.FindAsync(id);
            
            if (Mantenimiento == null)
            {
                return NotFound();
            }

            Mantenimiento.Descripcion = MantenimientoDto.Descripcion;
            Mantenimiento.Estado = MantenimientoDto.Estado;

            _context.Entry(Mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MantenimientoExists(id))
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

        // POST: api/Mantenimientos
        [HttpPost]
        public async Task<ActionResult<Mantenimiento>> PostMantenimiento(MantenimientoPostDto MantenimientoDto)
        {
            var Mantenimiento = _mapper.Map<Mantenimiento>(MantenimientoDto);
            _context.Mantenimientos.Add(Mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMantenimiento", new { id = Mantenimiento.Id }, Mantenimiento);
        }

        // DELETE: api/Mantenimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMantenimiento(int id)
        {
            var Mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (Mantenimiento == null)
            {
                return NotFound();
            }

            _context.Mantenimientos.Remove(Mantenimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MantenimientoExists(int id)
        {
            return _context.Mantenimientos.Any(e => e.Id == id);
        }
    }
}
