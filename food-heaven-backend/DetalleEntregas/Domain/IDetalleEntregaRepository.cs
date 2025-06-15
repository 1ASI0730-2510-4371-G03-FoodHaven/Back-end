using food_heaven_backend.Shared.Domain.Repositories;
using food_heaven_backend.DetalleEntregas.Domain.Models.Entities;

namespace food_heaven_backend.DetalleEntregas.Domain;
public interface IDetalleEntregaRepository : IBaseRepository<DetalleEntrega>
{
    Task<IEnumerable<DetalleEntrega>> ListByEstadosValidosAsync(); // solo "Pendiente", "En camino", "Entregado"
}
