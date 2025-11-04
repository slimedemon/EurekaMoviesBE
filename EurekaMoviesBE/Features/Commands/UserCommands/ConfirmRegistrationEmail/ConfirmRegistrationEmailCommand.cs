

namespace EurekaMoviesBE.Features.Commands.UserCommands.ConfirmRegistrationEmail;

public class ConfirmRegistrationEmailCommand : IRequest<ConfirmRegistrationEmailResponse>
{
    public ConfirmRegistrationEmailRequest Payload { get; set; }
    public ConfirmRegistrationEmailCommand(ConfirmRegistrationEmailRequest payload)
    {
        Payload = payload;
    }
}