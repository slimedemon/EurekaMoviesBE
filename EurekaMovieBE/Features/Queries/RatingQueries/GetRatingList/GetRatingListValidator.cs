using FluentValidation;
using EurekaMovieBE.Features.Queries.RatingQueries.GetReviews;

namespace EurekaMovieBE.Features.Queries.RatingQueries.GetRatingList;

public class GetRatingListValidator : AbstractValidator<GetRatingListQuery>
{
    public GetRatingListValidator()
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