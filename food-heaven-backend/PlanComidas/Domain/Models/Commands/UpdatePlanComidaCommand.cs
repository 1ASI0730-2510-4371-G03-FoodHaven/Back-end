namespace food_heaven_backend.PlanComidas.Domain.Models.Commands;

public record UpdatePlanComidaCommand(
    int Id,
    int IdUsuario,
    DateTime FechaInicio,
    DateTime FechaFin
);
