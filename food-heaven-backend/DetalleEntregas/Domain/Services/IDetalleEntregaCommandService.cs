using food_heaven_backend.DetalleEntregas.Domain.Models.Commands;
using food_heaven_backend.DetalleEntregas.Domain.Models.Entities;

namespace food_heaven_backend.DetalleEntregas.Domain.Services
{
    public interface IDetalleEntregaCommandService
    {
        Task<DetalleEntrega> Handle(CreateDetalleEntregaCommand command);
        Task<bool> Handle(UpdateDetalleEntregaCommand command, int id);
        Task<bool> Handle(DeleteDetalleEntregaCommand command);
    }
}