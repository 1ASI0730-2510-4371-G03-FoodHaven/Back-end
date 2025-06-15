using food_heaven_backend.DetalleEntregas.Domain.Models.Entities;
using food_heaven_backend.DetalleEntregas.Domain.Models.Queries;


namespace food_heaven_backend.DetalleEntregas.Domain.Services
{
    public interface IDetalleEntregaQueryService
    {
        Task<IEnumerable<DetalleEntrega>> Handle(GetAllDetalleEntregaQuery query);
        Task<DetalleEntrega> Handle(GetDetalleEntregaByIdQuery query);
    }
}