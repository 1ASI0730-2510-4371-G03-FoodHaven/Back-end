using System;
using System.ComponentModel.DataAnnotations.Schema;
using food_heaven_backend.Shared.Domain.Model.Entities;

namespace food_heaven_backend.Usuarios.Domain.Models.Entities
{
    [Table("usuario")] // nombre real de la tabla
    public class Usuario : BaseEntity
    {
        [Column("id_usuario")]
        public new int Id { get; set; }

        [Column("nombre")] public string Nombre { get; set; }
        [Column("email")] public string Email { get; set; }
        [Column("contraseña")] public string Contraseña { get; set; }
        [Column("edad")] public int Edad { get; set; }
        [Column("sexo")] public char Sexo { get; set; }
        [Column("distrito")] public string Distrito { get; set; }
        [Column("fecha_registro")] public DateTime FechaRegistro { get; set; }

        // 🔽 CONSTRUCTOR QUE NECESITAS
        public Usuario(string nombre, string email, string contraseña, int edad, char sexo, string distrito, DateTime fechaRegistro)
        {
            Nombre = nombre;
            Email = email;
            Contraseña = contraseña;
            Edad = edad;
            Sexo = sexo;
            Distrito = distrito;
            FechaRegistro = fechaRegistro;
        }
    }

}