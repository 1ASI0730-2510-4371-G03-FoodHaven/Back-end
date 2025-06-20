using food_heaven_backend.PlanComidas.Domain.Models.Entities;
using food_heaven_backend.PlanComidas.Domain.Services;
using food_heaven_backend.Shared.Infraestructure.Persistence.Configuration;
using food_heaven_backend.Shared.Infraestructure.Persistence.Repositories;

namespace food_heaven_backend.PlanComidas.Infrastructure;

public class PlanComidaRepository(FoodHeavenContext context)
    : BaseRepository<PlanComida>(context), IPlanComidaRepository { }
