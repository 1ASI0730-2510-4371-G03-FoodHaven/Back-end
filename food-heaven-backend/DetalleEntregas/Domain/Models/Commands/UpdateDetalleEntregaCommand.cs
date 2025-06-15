namespace food_heaven_backend.DetalleEntregas.Domain.Models.Commands
{
    public record UpdateDetalleEntregaCommand(
        int Id,
        int IdPedido,
        string DireccionEntrega,
        string Referencia,
        DateTime Fecha,
        TimeSpan Hora,
        string Estado
    );
}