using FluentValidation;

namespace EurekaMoviesBE.Features.Queries.RecommendationQueries.LLMSearch;

public class LLMSearchValidator : AbstractValidator<LLMSearchQuery>
{
    public LLMSearchValidator()
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
        
        RuleFor(command => command.Payload.Amount)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .InclusiveBetween(2, 100)
            .WithMessage("Amount must be between 2 and 100");
        
        RuleFor(command => command.Payload.Threshold)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .InclusiveBetween(0, 1)
            .WithMessage("Threshold must be between 0 and 1");

    }
}