using food_heaven_backend.Shared.Domain.Repositories;
using food_heaven_backend.Usuarios.Domain.Models.Entities;

namespace food_heaven_backend.Usuarios.Domain;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<Usuario?> FindByEmailAsync(string email);
}