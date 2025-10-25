using FluentValidation;

namespace EurekaMovieBE.Features.Queries.MovieQueries.GetPopularMovies;

public class GetPopularMoviesValidator : AbstractValidator<GetPopularMoviesQuery>
{
    public GetPopularMoviesValidator()
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
        
        RuleFor(command => command.Payload.PageNumber)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");
        
        RuleFor(command => command.Payload.MaxPerPage)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Max per page must be greater than 0.");
    }
}