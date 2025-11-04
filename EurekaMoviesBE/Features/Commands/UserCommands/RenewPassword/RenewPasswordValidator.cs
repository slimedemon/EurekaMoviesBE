using FluentValidation;

namespace EurekaMoviesBE.Features.Commands.UserCommands.RenewPassword;

public class RenewPasswordValidator : AbstractValidator<RenewPasswordCommand>
{
    public RenewPasswordValidator()
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
        
        RuleFor(command => command.Payload.NewPassword)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password is required.");
        
        RuleFor(command => command.Payload.ResetCode)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Reset code is required.");
    }
}