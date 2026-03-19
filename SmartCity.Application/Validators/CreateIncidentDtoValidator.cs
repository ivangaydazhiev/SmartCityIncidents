using FluentValidation;
using SmartCity.Application.DTOs;

namespace SmartCity.Application.Validators
{
    public class CreateIncidentDtoValidator : AbstractValidator<CreateIncidentDto>
    {
        public CreateIncidentDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid incident type");

            RuleFor(x => x.LocationId)
                .NotEmpty()
                .WithMessage("LocationId is required");
        }
    }
}
