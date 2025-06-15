using food_heaven_backend.Usuarios.Domain.Models.Commands;
using food_heaven_backend.Usuarios.Domain.Models.Entities;

namespace food_heaven_backend.Usuarios.Domain.Services;

public interface IUsuarioCommandService
{
    Task<Usuario> Handle(CreateUsuarioCommand command);
    Task<bool> Handle(UpdateUsuarioCommand command, int id);
    Task<bool> Handle(DeleteUsuarioCommand command);
}