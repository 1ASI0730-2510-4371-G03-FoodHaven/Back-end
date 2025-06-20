using food_heaven_backend.PlanComidas.Domain.Models.Entities;
using food_heaven_backend.PlanComidas.Domain.Models.Queries;
using food_heaven_backend.PlanComidas.Domain.Services;

namespace food_heaven_backend.PlanComidas.Application.QueryServices;

public class PlanComidaQueryService(IPlanComidaRepository repository) : IPlanComidaQueryService
{
    private readonly IPlanComidaRepository _repository = repository;

    public async Task<IEnumerable<PlanComida>> Handle(GetAllPlanComidasQuery query)
    {
        var items = await _repository.ListAsync();
        return items ?? Enumerable.Empty<PlanComida>();
    }

    public async Task<PlanComida?> Handle(GetPlanComidaByIdQuery query)
    {
        if (query == null) throw new ArgumentNullException(nameof(query));
        return await _repository.FindByIdAsync(query.Id);
    }
}
