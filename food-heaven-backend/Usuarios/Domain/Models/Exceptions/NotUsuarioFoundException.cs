namespace food_heaven_backend.Usuarios.Domain.Models.Exceptions;

public class NotUsuarioFoundException : Exception
{
    public NotUsuarioFoundException() 
        : base("Usuario no encontrado.")
    {
    }

    public NotUsuarioFoundException(string message) 
        : base(message)
    {
    }

    public NotUsuarioFoundException(string message, Exception inner) 
        : base(message, inner)
    {
    }
}