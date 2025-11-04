using FluentValidation;

namespace EurekaMoviesBE.Features.Commands.WatchListCommands.AddMovieToWatchList;

public class AddMovieToWatchListValidator : AbstractValidator<AddMovieToWatchListCommand>
{
    public AddMovieToWatchListValidator()
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
            .WithMessage("TmdbId is required.");
    }
}