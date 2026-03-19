using FluentValidation;
using SmartCity.Application.DTOs;

namespace SmartCity.Application.Validators
{
    public class UpdateIncidentDtoValidator : AbstractValidator<UpdateIncidentDto>
    {
        public UpdateIncidentDtoValidator(SmartCityDbContext context)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Type)
                .IsInEnum();

            RuleFor(x => x.Status)
                .IsInEnum();

            RuleFor(x => x.LocationId)
                .MustAsync(async (id, cancellation) =>
                    await context.Locations.AnyAsync(l => l.id == id, cancellation))
                .WithMessage("Location does not exist");

        }
    }
}
