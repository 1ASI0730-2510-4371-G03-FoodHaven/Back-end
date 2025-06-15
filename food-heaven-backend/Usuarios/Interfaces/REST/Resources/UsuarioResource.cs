namespace food_heaven_backend.Usuarios.Interfaces.REST.Resources;

public record UsuarioResource(
    int Id,
    string Nombre,
    string Email,
    string Contraseña,
    int Edad,
    char Sexo,
    string Distrito,
    DateTime FechaRegistro
);