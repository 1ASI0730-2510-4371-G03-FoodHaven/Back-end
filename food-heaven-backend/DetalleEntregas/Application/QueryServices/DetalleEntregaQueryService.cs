using food_heaven_backend.DetalleEntregas.Domain;
using food_heaven_backend.DetalleEntregas.Domain.Models.Queries;
using food_heaven_backend.DetalleEntregas.Domain.Services;
using food_heaven_backend.DetalleEntregas.Domain.Models.Entities;

namespace food_heaven_backend.DetalleEntregas.Application.QueryServices;

public class DetalleEntregaQueryService(IDetalleEntregaRepository detalleEntregaRepository)
    : IDetalleEntregaQueryService
{
    private readonly IDetalleEntregaRepository _detalleEntregaRepository = detalleEntregaRepository 
                                                                           ?? throw new ArgumentNullException(nameof(detalleEntregaRepository));

    public async Task<IEnumerable<DetalleEntrega>> Handle(GetAllDetalleEntregaQuery query)
    {
        var detalles = await _detalleEntregaRepository.ListAsync();
        return detalles ?? Enumerable.Empty<DetalleEntrega>();
    }

    public async Task<DetalleEntrega?> Handle(GetDetalleEntregaByIdQuery query)
    {
        if (query == null) throw new ArgumentNullException(nameof(query));

        var detalle = await _detalleEntregaRepository.FindByIdAsync(query.DetalleEntregaId);
        return detalle;
    }
}