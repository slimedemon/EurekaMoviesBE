using FluentValidation;

namespace EurekaMoviesBE.Features.Queries.RecommendationQueries.RAGSearch;

public class RAGSearchValidator : AbstractValidator<RAGSearchQuery>
{
    public RAGSearchValidator()
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
            .WithMessage("Payload is required");
        
        RuleFor(command => command.Payload.Collection)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Collection is required");
        
        RuleFor(command => command.Payload.Query)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MaximumLength(5000)
            .WithMessage("Query is required");
    }
}