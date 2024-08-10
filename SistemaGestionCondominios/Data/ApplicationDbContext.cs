using Microsoft.EntityFrameworkCore;
using SistemaGestionCondominios.Models;

namespace SistemaGestionCondominios.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Residencia> Residencias { get; set; } = default!;
        public DbSet<Documento> Documentos { get; set; } = default!;
        public DbSet<Encuesta> Encuestas { get; set; } = default!;
        public DbSet<Mantenimiento> Mantenimientos { get; set; } = default!;
        public DbSet<Notificacion> Notificaciones { get; set; } = default!;
        public DbSet<Pago> Pagos { get; set; } = default!;
        public DbSet<Reserva> Reservas { get; set; } = default!;
        public DbSet<RespuestaEncuesta> RespuestaEncuestas { get; set; } = default!;
        public DbSet<Usuario> Usuarios { get; set; } = default!;
        public DbSet<Visita> Visitas { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documento>()
                .Property(p => p.FechaSubida)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Encuesta>()
                .Property(p => p.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Mantenimiento>()
                .Property(p => p.FechaSolicitud)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Notificacion>()
                .Property(p => p.FechaEnvio)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Pago>()
                .Property(p => p.FechaPago)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reserva>()
                .Property(p => p.FechaReserva)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Usuario>()
                .Property(p => p.FechaRegistro)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Visita>()
                .Property(p => p.FechaVisita)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
