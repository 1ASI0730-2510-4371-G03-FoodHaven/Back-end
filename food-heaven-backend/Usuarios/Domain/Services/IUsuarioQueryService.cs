using food_heaven_backend.Usuarios.Domain.Models.Entities;
using food_heaven_backend.Usuarios.Domain.Models.Queries;

namespace food_heaven_backend.Usuarios.Domain.Services;

public interface IUsuarioQueryService
{
    Task<IEnumerable<Usuario>> Handle(GetAllUsuariosQuery query);
    Task<Usuario> Handle(GetUsuarioByIdQuery query);
}