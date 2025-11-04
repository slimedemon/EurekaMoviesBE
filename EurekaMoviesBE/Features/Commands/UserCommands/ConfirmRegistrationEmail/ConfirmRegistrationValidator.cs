using FluentValidation;

namespace EurekaMoviesBE.Features.Commands.UserCommands.ConfirmRegistrationEmail;

public class ConfirmRegistrationValidator : AbstractValidator<ConfirmRegistrationEmailCommand>
{
    public ConfirmRegistrationValidator()
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
        
        RuleFor(command => command.Payload.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required.");
        
        RuleFor(command => command.Payload.VerificationToken)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("VerificationToken is required.");
    }
}