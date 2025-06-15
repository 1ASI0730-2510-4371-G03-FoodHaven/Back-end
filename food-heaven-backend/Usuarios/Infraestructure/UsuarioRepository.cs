using food_heaven_backend.Shared.Domain.Repositories;
using food_heaven_backend.Shared.Infraestructure.Persistence.Configuration;
using food_heaven_backend.Shared.Infraestructure.Persistence.Repositories;
using food_heaven_backend.Usuarios.Domain;
using food_heaven_backend.Usuarios.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace food_heaven_backend.Usuarios.Infraestructure;

public class UsuarioRepository(FoodHeavenContext context)
    : BaseRepository<Usuario>(context), IUsuarioRepository
{
    public async Task<Usuario?> FindByEmailAsync(string email)
    {
        return await Context.Set<Usuario>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}