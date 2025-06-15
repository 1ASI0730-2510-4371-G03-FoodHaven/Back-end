using food_heaven_backend.Shared.Domain.Repositories;
using food_heaven_backend.Usuarios.Domain;
using food_heaven_backend.Usuarios.Domain.Models.Commands;
using food_heaven_backend.Usuarios.Domain.Models.Entities;
using food_heaven_backend.Usuarios.Domain.Models.Queries;
using food_heaven_backend.Usuarios.Domain.Services;

namespace food_heaven_backend.Usuarios.Application.QueryServices;

public class UsuarioQueryService(IUsuarioRepository usuarioRepository) : IUsuarioQueryService
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));

    public async Task<IEnumerable<Usuario>> Handle(GetAllUsuariosQuery query)
    {
        var usuarios = await _usuarioRepository.ListAsync();
        return usuarios ?? Enumerable.Empty<Usuario>();
    }

    public async Task<Usuario?> Handle(GetUsuarioByIdQuery query)
    {
        if (query == null) throw new ArgumentNullException(nameof(query));

        var usuario = await _usuarioRepository.FindByIdAsync(query.UsuarioId);
        return usuario;
    }
}