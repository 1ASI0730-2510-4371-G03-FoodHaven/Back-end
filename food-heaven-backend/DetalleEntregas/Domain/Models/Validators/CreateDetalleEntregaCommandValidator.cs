using FluentValidation;
using food_heaven_backend.DetalleEntregas.Domain.Models.Commands;

namespace food_heaven_backend.DetalleEntregas.Domain.Models.Validators
{
    public class CreateDetalleEntregaCommandValidator : AbstractValidator<CreateDetalleEntregaCommand>
    {
        public CreateDetalleEntregaCommandValidator()
        {
            RuleFor(e => e.IdPedido)
                .GreaterThan(0).WithMessage("El ID del pedido debe ser mayor que 0");

            RuleFor(e => e.DireccionEntrega)
                .NotEmpty().WithMessage("La dirección de entrega es obligatoria")
                .MaximumLength(255).WithMessage("La dirección no puede exceder los 255 caracteres");

            RuleFor(e => e.Referencia)
                .MaximumLength(1000).WithMessage("La referencia no puede exceder los 1000 caracteres");

            RuleFor(e => e.Fecha)
                .NotEmpty().WithMessage("La fecha de entrega es obligatoria");

            RuleFor(e => e.Hora)
                .NotEmpty().WithMessage("La hora de entrega es obligatoria");

            RuleFor(e => e.Estado)
                .NotEmpty().WithMessage("El estado es obligatorio")
                .MaximumLength(50).WithMessage("El estado no puede exceder los 50 caracteres");
        }
    }
}