using FluentValidation;
using food_heaven_backend.FoodCatalogContext.Domain.Models.Commands;

namespace food_heaven_backend.FoodCatalogContext.Domain.Models.Validators;

public class CreateComidaCommandValidator : AbstractValidator<CreateComidaCommand>
{
    public CreateComidaCommandValidator()
    {
        RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("Nombre is required")
            .MaximumLength(50).WithMessage("Nombre must be at most 50 characters");

        RuleFor(p => p.Descripcion)
            .NotEmpty().WithMessage("Descripcion is required")
            .MaximumLength(50).WithMessage("Descripcion must be at most 50 characters");
        
    }
    
}