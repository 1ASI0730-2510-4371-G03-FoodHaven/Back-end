using food_heaven_backend.Usuarios.Domain.Models.Entities;
using food_heaven_backend.Usuarios.Interfaces.REST.Resources;

namespace food_heaven_backend.Usuarios.Interfaces.REST.Transform;

public static class UsuarioResourceFromEntityAssembler
{
    public static UsuarioResource ToResourceFromEntity(Usuario usuario)
    {
        return new UsuarioResource(
            usuario.Id,
            usuario.Nombre,
            usuario.Email,
            usuario.Contraseña,
            usuario.Edad,
            usuario.Sexo,
            usuario.Distrito,
            usuario.FechaRegistro
        );
    }
}