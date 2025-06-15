using food_heaven_backend.DetalleEntregas.Domain;
using food_heaven_backend.DetalleEntregas.Domain.Models.Entities;
using food_heaven_backend.Shared.Infraestructure.Persistence.Configuration;
using food_heaven_backend.Shared.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace food_heaven_backend.DetalleEntregas.Infraestructure;

public class DetalleEntregaRepository(FoodHeavenContext context)
    : BaseRepository<DetalleEntrega>(context), IDetalleEntregaRepository
{
    public async Task<IEnumerable<DetalleEntrega>> ListByEstadosValidosAsync()
    {
        var estadosValidos = new[] { "Pendiente", "En camino", "Entregado" };

        return await Context.Set<DetalleEntrega>()
            .Where(e => estadosValidos.Contains(e.Estado))
            .ToListAsync();
    }
}
