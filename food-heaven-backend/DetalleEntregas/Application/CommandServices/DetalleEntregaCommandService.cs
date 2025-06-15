using System.Data;
using FluentValidation;
using food_heaven_backend.DetalleEntregas.Domain;
using food_heaven_backend.DetalleEntregas.Domain.Models.Commands;
using food_heaven_backend.Shared.Domain.Repositories;
using food_heaven_backend.DetalleEntregas.Domain.Services;
using food_heaven_backend.DetalleEntregas.Domain.Models.Entities;

namespace food_heaven_backend.DetalleEntregas.Application.CommandServices;

public class DetalleEntregaCommandService(
    IDetalleEntregaRepository detalleEntregaRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateDetalleEntregaCommand> validator)
    : IDetalleEntregaCommandService
{
    private readonly IDetalleEntregaRepository _detalleEntregaRepository = detalleEntregaRepository ?? throw new ArgumentNullException(nameof(detalleEntregaRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IValidator<CreateDetalleEntregaCommand> _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    public async Task<DetalleEntrega> Handle(CreateDetalleEntregaCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var validationResult = await _validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationException(string.Join(", ", errors));
        }

        var detalleEntrega = new DetalleEntrega(
            command.IdPedido,
            command.DireccionEntrega,
            command.Referencia,
            command.Fecha,
            command.Hora,
            command.Estado
        );

        await _detalleEntregaRepository.AddAsync(detalleEntrega);
        await _unitOfWork.CompleteAsync();

        return detalleEntrega;
    }

    public async Task<bool> Handle(UpdateDetalleEntregaCommand command, int id)
    {
        var detalleEntrega = await _detalleEntregaRepository.FindByIdAsync(id);
        if (detalleEntrega == null)
            throw new DataException("Detalle de entrega no encontrado.");

        detalleEntrega.IdPedido = command.IdPedido;
        detalleEntrega.DireccionEntrega = command.DireccionEntrega;
        detalleEntrega.Referencia = command.Referencia;
        detalleEntrega.Fecha = command.Fecha;
        detalleEntrega.Hora = command.Hora;
        detalleEntrega.Estado = command.Estado;

        _detalleEntregaRepository.Update(detalleEntrega);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> Handle(DeleteDetalleEntregaCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var detalleEntrega = await _detalleEntregaRepository.FindByIdAsync(command.Id);
        if (detalleEntrega is null) return false;

        _detalleEntregaRepository.Remove(detalleEntrega);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}
