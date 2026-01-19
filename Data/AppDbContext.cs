using Challenge_ABM_Clientes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Challenge_ABM_Clientes.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.Nombre)
                .HasColumnName("nombre");

            entity.Property(e => e.Apellido)
                .HasColumnName("apellido");

            entity.Property(e => e.RazonSocial)
                .HasColumnName("razon_social");

            entity.Property(e => e.CUIT)
                .HasColumnName("cuit");

            entity.Property(e => e.FechaNacimiento)
                .HasColumnName("fecha_nacimiento");

            entity.Property(e => e.TelefonoCelular)
                .HasColumnName("telefono_celular");

            entity.Property(e => e.Email)
                .HasColumnName("email");
        });
    }
}
