using MediatR;
using EurekaMovieBE.Dtos.Requests;
using EurekaMovieBE.Dtos.Responses;

namespace EurekaMovieBE.Features.Commands.UserCommands.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public LoginRequest Payload { get; set; }
    public LoginCommand(LoginRequest payload)
    {
        Payload = payload;
    }
}