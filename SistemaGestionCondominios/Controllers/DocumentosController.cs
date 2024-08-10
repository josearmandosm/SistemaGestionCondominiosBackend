using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Documento;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public DocumentosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Documentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentoDto>>> GetDocumento()
        {
            var Documentos = await _context.Documentos.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<DocumentoDto>>(Documentos));
        }

        // GET: api/Documentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentoDto>> GetDocumento(int id)
        {
            var Documento = await _context.Documentos.FindAsync(id);

            if (Documento == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocumentoDto>(Documento));
        }

        // PUT: api/Documentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumento(int id, DocumentoPutDto DocumentoDto)
        {
            if (id != DocumentoDto.Id)
            {
                return BadRequest();
            }

            var Documento = await _context.Documentos.FindAsync(id);
            
            if (Documento == null)
            {
                return NotFound();
            }

            Documento.Titulo = DocumentoDto.Titulo;
            Documento.Descripcion = DocumentoDto.Descripcion;
            Documento.RutaArchivo = DocumentoDto.RutaArchivo;

            _context.Entry(Documento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoExists(id))
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

        // POST: api/Documentos
        [HttpPost]
        public async Task<ActionResult<Documento>> PostDocumento(DocumentoPostDto DocumentoDto)
        {
            var Documento = _mapper.Map<Documento>(DocumentoDto);
            _context.Documentos.Add(Documento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocumento", new { id = Documento.Id }, Documento);
        }

        // DELETE: api/Documentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            var Documento = await _context.Documentos.FindAsync(id);
            if (Documento == null)
            {
                return NotFound();
            }

            _context.Documentos.Remove(Documento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentoExists(int id)
        {
            return _context.Documentos.Any(e => e.Id == id);
        }
    }
}
