using FluentValidation;
using food_heaven_backend.Usuarios.Domain.Models.Commands;

namespace food_heaven_backend.Usuarios.Domain.Models.Validators;

public class CreateUsuarioCommandValidator : AbstractValidator<CreateUsuarioCommand>
{
    public CreateUsuarioCommandValidator()
    {
        RuleFor(u => u.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("El correo es obligatorio")
            .EmailAddress().WithMessage("Debe ser un correo válido");

        RuleFor(u => u.Contraseña)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

        RuleFor(u => u.Edad)
            .InclusiveBetween(0, 150).WithMessage("Edad debe estar entre 0 y 150");

        RuleFor(u => u.Sexo)
            .Must(s => s == 'M' || s == 'F').WithMessage("Sexo debe ser 'M' o 'F'");

        RuleFor(u => u.Distrito)
            .NotEmpty().WithMessage("El distrito es obligatorio")
            .MaximumLength(100).WithMessage("El distrito no puede exceder los 100 caracteres");
    }
}