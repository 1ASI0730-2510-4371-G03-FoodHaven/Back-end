namespace food_heaven_backend.DetalleEntregas.Interfaces.REST.Resources;

public record DetalleEntregaResource(
    int IdEntrega,
    int IdPedido,
    string DireccionEntrega,
    string Referencia,
    DateTime Fecha,
    TimeSpan Hora,
    string Estado
);