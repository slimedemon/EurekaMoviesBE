using MediatR;
using EurekaMovieBE.Dtos.Requests;
using EurekaMovieBE.Dtos.Responses;

namespace EurekaMovieBE.Features.Commands.UserCommands.LoginSocial;

public class LoginSocialCommand : IRequest<LoginSocialResponse>
{
    public LoginSocialRequest Payload { get; set; }
    public LoginSocialCommand(LoginSocialRequest payload)
    {
        Payload = payload;
    }
}