using System.Data;
using FluentValidation;
using food_heaven_backend.Shared.Domain.Repositories;
using food_heaven_backend.Usuarios.Domain;
using food_heaven_backend.Usuarios.Domain.Models.Commands;
using food_heaven_backend.Usuarios.Domain.Models.Entities;
using food_heaven_backend.Usuarios.Domain.Models.Exceptions;
using food_heaven_backend.Usuarios.Domain.Services;

namespace food_heaven_backend.Usuarios.Application.CommandServices;

public class UsuarioCommandService(
    IUsuarioRepository usuarioRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateUsuarioCommand> validator)
    : IUsuarioCommandService
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IValidator<CreateUsuarioCommand> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public async Task<Usuario> Handle(CreateUsuarioCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var validationResult = await _validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationException(string.Join(", ", errors));
        }

        var existingUser = await _usuarioRepository.FindByEmailAsync(command.Email);
        if (existingUser != null)
            throw new DuplicateNameException($"Email '{command.Email}' already registered.");

        var usuario = new Usuario(
            command.Nombre,
            command.Email,
            command.Contraseña,
            command.Edad,
            command.Sexo,
            command.Distrito,
            DateTime.UtcNow // FechaRegistro
        );

        await _usuarioRepository.AddAsync(usuario);
        await _unitOfWork.CompleteAsync();

        return usuario;
    }

    public async Task<bool> Handle(UpdateUsuarioCommand command, int id)
    {
        var usuario = await _usuarioRepository.FindByIdAsync(id);
        if (usuario == null) throw new DataException("Usuario not found.");

        usuario.Nombre = command.Nombre;
        usuario.Email = command.Email;
        usuario.Contraseña = command.Contraseña;
        usuario.Edad = command.Edad;
        usuario.Sexo = command.Sexo;
        usuario.Distrito = command.Distrito;

        _usuarioRepository.Update(usuario);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> Handle(DeleteUsuarioCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var usuario = await _usuarioRepository.FindByIdAsync(command.Id);
        if (usuario is null) return false;

        _usuarioRepository.Remove(usuario); // Eliminación real
        await _unitOfWork.CompleteAsync();

        return true;
    }
}
