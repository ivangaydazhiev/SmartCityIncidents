using FluentValidation;
using SmartCity.Application.DTOs;
using SmartCity.Application.Interfaces;

namespace SmartCity.Application.Validators
{
    public class UpdateIncidentDtoValidator : AbstractValidator<UpdateIncidentDto>
    {
        public UpdateIncidentDtoValidator(ILocationRepository locationRepository)
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
                .MustAsync(async (locationId, cancellation) =>
                    await locationRepository.ExistAsync(locationId))
                .WithMessage("Location does not exist");

        }
    }
}
