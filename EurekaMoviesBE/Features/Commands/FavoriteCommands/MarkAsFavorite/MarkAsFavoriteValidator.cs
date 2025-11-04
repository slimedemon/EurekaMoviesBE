using FluentValidation;

namespace EurekaMoviesBE.Features.Commands.FavoriteCommands.MarkAsFavorite;

public class MarkAsFavoriteValidator : AbstractValidator<MarkAsFavoriteCommand>
{
    public MarkAsFavoriteValidator()
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
    }
}