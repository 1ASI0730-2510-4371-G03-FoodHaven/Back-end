using System.Data;
using FluentValidation;
using food_heaven_backend.FoodCatalogContext.Domain.Models.Commands;
using food_heaven_backend.FoodCatalogContext.Domain.Services;
using food_heaven_backend.Shared.Domain.Repositories;
using food_heaven_backend.FoodCatalogContext.Domain.Models.Entities;

namespace food_heaven_backend.FoodCatalogContext.Application.CommandServices;

public class ComidaCommandService(
    IComidaRepository comidaRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateComidaCommand> validator)
    : IComidaCommandService
{
    private readonly IComidaRepository _comidaRepository = comidaRepository ?? throw new ArgumentNullException(nameof(comidaRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IValidator<CreateComidaCommand> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
   

    public async Task<Comida> Handle(CreateComidaCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var validationResult = await _validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationException(string.Join(", ", errors));
        }

        var comida = new Comida(
            command.Nombre,
            command.Descripcion,
            command.Calorias,
            command.Id_TipoComida,
            command.Id_proveedor,
            command.EsEspecial
            
        );

        await _comidaRepository.AddAsync(comida);
        await _unitOfWork.CompleteAsync();

        return comida;
    }

   

    public async Task<bool> Handle(DeleteComidaCommand command)
    {
        
        ArgumentNullException.ThrowIfNull(command);

        var comida = await _comidaRepository.FindByIdAsync(command.Id);
        if (comida is null) return false;

        _comidaRepository.Remove(comida);
        await _unitOfWork.CompleteAsync();

        return true;
        
    }

    public async Task<bool> Handle(UpdateComidaCommand command, int id)
    {
        var comida = await _comidaRepository.FindByIdAsync(id);
        if (comida == null)
            throw new DataException("Comida no encontrada.");
        
        comida.Nombre = command.Nombre;
        comida.Descripcion = command.Descripcion;
        comida.Calorias = command.Calorias;
        comida.Id_TipoComida = command.Id_TipoComida;
        comida.EsEspecial = command.EsEspecial;

        comida.Id_Proveedor = command.Id_proveedor;

        _comidaRepository.Update(comida);
        await _unitOfWork.CompleteAsync();

        return true;
        
    }

    
}
