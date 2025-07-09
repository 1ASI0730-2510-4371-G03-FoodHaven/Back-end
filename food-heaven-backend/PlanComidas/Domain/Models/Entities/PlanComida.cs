using System.ComponentModel.DataAnnotations.Schema;
using food_heaven_backend.Shared.Domain.Model.Entities;

namespace food_heaven_backend.PlanComidas.Domain.Models.Entities;

[Table("PlanComida")]
public class PlanComida : BaseEntity
{
    [Column("id_plan")]
    public new int Id { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("fecha_inicio")]
    public DateTime FechaInicio { get; set; }

    [Column("fecha_fin")]
    public DateTime FechaFin { get; set; }

    public PlanComida(int idUsuario, DateTime fechaInicio, DateTime fechaFin)
    {
        IdUsuario = idUsuario;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
    }

    public PlanComida() { }
}
