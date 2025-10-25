using FluentValidation;

namespace EurekaMovieBE.Features.Commands.RatingCommands.AddRating;

public class AddRatingValidator : AbstractValidator<AddRatingCommand>
{
    public AddRatingValidator()
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
        
        RuleFor(command => command.Payload.TmdbId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("TmdbId is required.");
        
        RuleFor(command => command.Payload.Stars)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .InclusiveBetween(1, 10)
            .WithMessage("Stars must be between 1 and 10.");
    }
}