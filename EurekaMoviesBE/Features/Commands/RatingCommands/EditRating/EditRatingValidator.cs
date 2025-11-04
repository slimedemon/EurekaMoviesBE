using FluentValidation;

namespace EurekaMoviesBE.Features.Commands.RatingCommands.EditRating;

public class EditRatingValidator : AbstractValidator<EditRatingCommand>
{
    public EditRatingValidator()
    {
        RuleFor(command => command)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Command cannot be null or empty.");
        
        RuleFor(command => command.Payload)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Payload cannot be null or empty.");
        
        RuleFor(command => command.Payload.RatingId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("RatingId is required.");
        
        RuleFor(command => command.Payload.Stars)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .InclusiveBetween(1, 10)
            .WithMessage("Stars must be between 1 and 10.");
    }
}