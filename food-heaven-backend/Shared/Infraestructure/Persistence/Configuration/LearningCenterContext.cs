using Microsoft.EntityFrameworkCore;
using food_heaven_backend.Usuarios.Domain.Models.Entities;
using food_heaven_backend.DetalleEntregas.Domain.Models.Entities;

namespace food_heaven_backend.Shared.Infraestructure.Persistence.Configuration
{
    public class FoodHeavenContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<DetalleEntrega> DetallesEntrega { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id).HasColumnName("id_usuario");
                entity.Property(u => u.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(100);
                entity.Property(u => u.Contraseña).HasColumnName("contraseña").IsRequired().HasMaxLength(100);
                entity.Property(u => u.Edad).HasColumnName("edad").IsRequired();
                entity.Property(u => u.Sexo).HasColumnName("sexo").IsRequired();
                entity.Property(u => u.Distrito).HasColumnName("distrito").HasMaxLength(100);
                entity.Property(u => u.FechaRegistro).HasColumnName("fecha_registro").IsRequired();
            });

            builder.Entity<DetalleEntrega>(entity =>
            {
                entity.ToTable("detalle_entrega");

                entity.HasKey(de => de.Id);

                entity.Property(de => de.Id).HasColumnName("id_entrega");
                entity.Property(de => de.IdPedido).HasColumnName("id_pedido");
                entity.Property(de => de.DireccionEntrega).HasColumnName("direccion_entrega").HasMaxLength(255);
                entity.Property(de => de.Referencia).HasColumnName("referencia");
                entity.Property(de => de.Fecha).HasColumnName("fecha");
                entity.Property(de => de.Hora).HasColumnName("hora");
                entity.Property(de => de.Estado).HasColumnName("estado").HasMaxLength(50);
            });
        }
    }
}
