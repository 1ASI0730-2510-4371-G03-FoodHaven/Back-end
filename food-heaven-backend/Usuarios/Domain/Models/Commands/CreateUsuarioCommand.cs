namespace food_heaven_backend.Usuarios.Domain.Models.Commands;

public record CreateUsuarioCommand(
    string Nombre,
    string Email,
    string Contraseña,
    int Edad,
    char Sexo,
    string Distrito
);