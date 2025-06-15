namespace food_heaven_backend.DetalleEntregas.Domain.Models.Exceptions
{
    public class NotDetalleEntregaFoundException : Exception
    {
        public NotDetalleEntregaFoundException() 
            : base("Detalle de entrega no encontrado.")
        {
        }

        public NotDetalleEntregaFoundException(string message) 
            : base(message)
        {
        }

        public NotDetalleEntregaFoundException(string message, Exception inner) 
            : base(message, inner)
        {
        }
    }
}