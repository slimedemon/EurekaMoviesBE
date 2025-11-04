using FluentValidation;

namespace EurekaMoviesBE.Features.Queries.RecommendationQueries.GetNavigation;

public class GetNavigationValidator : AbstractValidator<GetNavigationQuery>
{
    public GetNavigationValidator()
    {
        RuleFor(command => command)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Command cannot be null or empty.");
        
        RuleFor(command => command.Query)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Query is required");
    }
}