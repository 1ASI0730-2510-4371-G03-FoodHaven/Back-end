namespace food_heaven_backend.Usuarios.Domain.Models.Commands;

public record UpdateUsuarioCommand(
    int Id,
    string Nombre,
    string Email,
    string Contraseña,
    int Edad,
    char Sexo,
    string Distrito
);