using FluentValidation;

namespace EurekaMovieBE.Features.Commands.UserCommands.LoginSocial;

public class LoginSocialValidator : AbstractValidator<LoginSocialCommand>
{
    public LoginSocialValidator()
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
        
        RuleFor(command => command.Payload.JwtString)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required.");
    }
}