using Microsoft.EntityFrameworkCore;
using food_heaven_backend.Usuarios.Domain.Models.Entities;
<<<<<<< HEAD:food-heaven-backend/Shared/Infraestructure/Persistence/Configuration/LearningCenterContext.cs
using food_heaven_backend.FoodCatalogContext.Domain.Models.Entities;
=======
using food_heaven_backend.PlanComidas.Domain.Models.Entities;
>>>>>>> feature/mealPlanContext:food-heaven-backend/Shared/Infraestructure/Persistence/Configuration/FoodHeavenContext.cs

namespace food_heaven_backend.Shared.Infraestructure.Persistence.Configuration
{
    public class FoodHeavenContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
<<<<<<< HEAD:food-heaven-backend/Shared/Infraestructure/Persistence/Configuration/LearningCenterContext.cs
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<TipoProveedor> TiposProveedor { get; set; }
        public DbSet<TipoComida> TipoComidas { get; set; }
        public DbSet<Comida> Comidas { get; set; }
=======
        public DbSet<PlanComida> PlanesComida { get; set; }
>>>>>>> feature/mealPlanContext:food-heaven-backend/Shared/Infraestructure/Persistence/Configuration/FoodHeavenContext.cs

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
            
<<<<<<< HEAD:food-heaven-backend/Shared/Infraestructure/Persistence/Configuration/LearningCenterContext.cs
            builder.Entity<TipoProveedor>(entity =>
            {
                entity.ToTable("tipo_proveedor");

                entity.HasKey(tp => tp.Id);

                entity.Property(tp => tp.Id).HasColumnName("id_tipo_proveedor");
                entity.Property(tp => tp.Descripcion).HasColumnName("Descripcion").IsRequired().HasMaxLength(100);
            });

            builder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("proveedor");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id).HasColumnName("id_proveedor");
                entity.Property(p => p.Nombre).HasColumnName("nombre").IsRequired().HasMaxLength(100);
                entity.Property(p => p.Distrito).HasColumnName("distrito").IsRequired().HasMaxLength(100);
                entity.Property(p => p.Contacto).HasColumnName("contacto").IsRequired().HasMaxLength(100);
                entity.Property(p => p.TipoProveedorId).HasColumnName("id_tipo_proveedor").IsRequired();

                entity.HasOne(p => p.TipoProveedor)
                      .WithMany()
                      .HasForeignKey(p => p.TipoProveedorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Comida>(entity =>
            {
                entity.ToTable("comida");

                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).HasColumnName("id_comida");

                entity.Property(c => c.Nombre)
                    .HasColumnName("nombre")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Descripcion)
                    .HasColumnName("descripcion")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(c => c.Calorias)
                    .HasColumnName("calorias")
                    .IsRequired();

                entity.Property(c => c.EsEspecial)
                    .HasColumnName("es_especial")
                    .IsRequired();

                entity.Property(c => c.Id_TipoComida)
                    .HasColumnName("id_tipo_comida")
                    .IsRequired();

                entity.Property(c => c.Id_Proveedor)
                    .HasColumnName("id_proveedor")
                    .IsRequired();

                entity.HasOne(c => c.TipoComida)
                    .WithMany()
                    .HasForeignKey(c => c.Id_TipoComida)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Proveedor)
                    .WithMany()
                    .HasForeignKey(c => c.Id_Proveedor)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<TipoComida>(entity =>
            {
                entity.ToTable("tipo_comida");

                entity.HasKey(tc => tc.Id);

                entity.Property(tc => tc.Id)
                    .HasColumnName("id_tipo_comida");

                entity.Property(tc => tc.Descripcion)
                    .HasColumnName("descripcion")
                    .IsRequired();
=======
            builder.Entity<PlanComida>(entity =>
            {
                entity.ToTable("plan_comida");

                entity.HasKey(pc => pc.Id);

                entity.Property(pc => pc.Id).HasColumnName("id_plan");
                entity.Property(pc => pc.IdUsuario).HasColumnName("id_usuario").IsRequired();
                entity.Property(pc => pc.FechaInicio).HasColumnName("fecha_inicio").IsRequired();
                entity.Property(pc => pc.FechaFin).HasColumnName("fecha_fin").IsRequired();
>>>>>>> feature/mealPlanContext:food-heaven-backend/Shared/Infraestructure/Persistence/Configuration/FoodHeavenContext.cs
            });
        }
    }
}