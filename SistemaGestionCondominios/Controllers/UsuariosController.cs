using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Auth;
using SistemaGestionCondominios.DTOs.Usuario;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context, IMapper mapper, IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuario()
        {
            var Usuarios = await _context.Usuarios.ToListAsync();

            if (Usuarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<UsuarioDto>>(Usuarios));
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var Usuario = await _context.Usuarios.FindAsync(id);

            if (Usuario == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UsuarioDto>(Usuario));
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioPutDto UsuarioDto)
        {
            if (id != UsuarioDto.Id)
            {
                return BadRequest();
            }

            var Usuario = await _context.Usuarios.FindAsync(id);
            
            if (Usuario == null)
            {
                return NotFound();
            }

            Usuario.Nombre = UsuarioDto.Nombre;
            Usuario.Apellido = UsuarioDto.Apellido;
            Usuario.Email = UsuarioDto.Email;
            Usuario.Telefono = UsuarioDto.Telefono;
            Usuario.Rol = UsuarioDto.Rol;

            if (!string.IsNullOrEmpty(UsuarioDto.Password))
            {
                Usuario.Password = _passwordHasher.HashPassword(Usuario, UsuarioDto.Password!);
            }

            _context.Entry(Usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioPostDto UsuarioDto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == UsuarioDto.Email))
            {
                return BadRequest("Email ya existe.");
            }

            var Usuario = _mapper.Map<Usuario>(UsuarioDto);
            Usuario.Password = _passwordHasher.HashPassword(Usuario, UsuarioDto.Password);
            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = Usuario.Id }, Usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var Usuario = await _context.Usuarios.FindAsync(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(Usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
