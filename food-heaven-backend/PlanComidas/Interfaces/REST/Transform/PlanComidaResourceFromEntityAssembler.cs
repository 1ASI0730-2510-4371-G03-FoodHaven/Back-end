using food_heaven_backend.PlanComidas.Domain.Models.Entities;
using food_heaven_backend.PlanComidas.Interfaces.REST.Resources;

namespace food_heaven_backend.PlanComidas.Interfaces.REST.Transform;

public static class PlanComidaResourceFromEntityAssembler
{
    public static PlanComidaResource ToResourceFromEntity(PlanComida plan)
    {
        return new PlanComidaResource(
            plan.Id,
            plan.IdUsuario,
            plan.FechaInicio,
            plan.FechaFin
        );
    }
}
