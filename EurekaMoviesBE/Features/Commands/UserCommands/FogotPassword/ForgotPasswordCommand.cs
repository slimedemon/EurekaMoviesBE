

namespace EurekaMoviesBE.Features.Commands.UserCommands.FogotPassword;

public class ForgotPasswordCommand : IRequest<ForgotPasswordResponse>
{
    public string Email { get; set; }
    public ForgotPasswordCommand(string email)
    {
        Email = email;
    }
}