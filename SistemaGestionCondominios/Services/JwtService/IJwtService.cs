namespace SistemaGestionCondominios.Services.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
        string? GetUsername(string token);
    }
}