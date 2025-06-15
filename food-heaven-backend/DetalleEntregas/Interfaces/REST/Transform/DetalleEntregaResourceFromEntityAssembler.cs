using food_heaven_backend.DetalleEntregas.Interfaces.REST.Resources;
using food_heaven_backend.DetalleEntregas.Domain.Models.Entities;

namespace food_heaven_backend.DetalleEntregas.Interfaces.REST.Transform
{
    public static class DetalleEntregaResourceFromEntityAssembler
    {
        public static DetalleEntregaResource ToResourceFromEntity(DetalleEntrega entrega)
        {
            return new DetalleEntregaResource(
                entrega.Id,
                entrega.IdPedido,
                entrega.DireccionEntrega,
                entrega.Referencia,
                entrega.Fecha,
                entrega.Hora,
                entrega.Estado
            );
        }
    }
}