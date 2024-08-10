using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Pago;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public PagosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Pagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagoDto>>> GetPago()
        {
            var Pagos = await _context.Pagos.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<PagoDto>>(Pagos));
        }

        // GET: api/Pagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PagoDto>> GetPago(int id)
        {
            var Pago = await _context.Pagos.FindAsync(id);

            if (Pago == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PagoDto>(Pago));
        }

        // PUT: api/Pagos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, PagoPutDto PagoDto)
        {
            if (id != PagoDto.Id)
            {
                return BadRequest();
            }

            var Pago = await _context.Pagos.FindAsync(id);
            
            if (Pago == null)
            {
                return NotFound();
            }

            Pago.Monto = PagoDto.Monto;
            Pago.Descripcion = PagoDto.Descripcion;
            Pago.TipoPago = PagoDto.TipoPago;
            Pago.DescripcionPago = PagoDto.DescripcionPago;
            Pago.Estado = PagoDto.Estado;

            _context.Entry(Pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // POST: api/Pagos
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(PagoPostDto PagoDto)
        {
            var Pago = _mapper.Map<Pago>(PagoDto);
            _context.Pagos.Add(Pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPago", new { id = Pago.Id }, Pago);
        }

        // DELETE: api/Pagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var Pago = await _context.Pagos.FindAsync(id);
            if (Pago == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(Pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.Id == id);
        }
    }
}
