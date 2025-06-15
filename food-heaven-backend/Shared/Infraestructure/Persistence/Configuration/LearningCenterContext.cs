using Microsoft.EntityFrameworkCore;
using food_heaven_backend.Usuarios.Domain.Models.Entities;

namespace food_heaven_backend.Shared.Infraestructure.Persistence.Configuration
{
    public class FoodHeavenContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario"); // Nombre real de la tabla

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .HasColumnName("id_usuario");

                entity.Property(u => u.Nombre)
                    .HasColumnName("nombre")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Contraseña)
                    .HasColumnName("contraseña")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Edad)
                    .HasColumnName("edad")
                    .IsRequired();

                entity.Property(u => u.Sexo)
                    .HasColumnName("sexo")
                    .IsRequired();

                entity.Property(u => u.Distrito)
                    .HasColumnName("distrito")
                    .HasMaxLength(100);

                entity.Property(u => u.FechaRegistro)
                    .HasColumnName("fecha_registro")
                    .IsRequired();
            });
        }
    }
}