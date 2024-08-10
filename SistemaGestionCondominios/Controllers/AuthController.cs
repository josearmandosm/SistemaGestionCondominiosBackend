using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Data;
using SistemaGestionCondominios.DTOs.Auth;
using SistemaGestionCondominios.Models;
using SistemaGestionCondominios.Services.JwtService;

namespace SistemaGestionCondominios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public AuthController(ApplicationDbContext context, IJwtService jwtService, IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }


        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarDto registrarDto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == registrarDto.Email))
            {
                return BadRequest("Email ya existe.");
            }

            var user = new Usuario
            {
                Email = registrarDto.Email,
                Rol = RolEnum.Visitante,
                Nombre = registrarDto.Nombre,
                Apellido = registrarDto.Apellido,
                Telefono = registrarDto.Telefono
            };

            user.Password = _passwordHasher.HashPassword(user, registrarDto.Password);

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado satisfactoriamente.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == loginDto.Username);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password) == PasswordVerificationResult.Failed)
            {
                return Unauthorized();
            }

            var token = _jwtService.GenerateToken(user.Email, user.Rol.ToString());

            return Ok(new { token });
        }
    }
}
