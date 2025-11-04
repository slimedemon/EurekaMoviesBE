using MediatR;
using EurekaMoviesBE.Dtos.Requests;
using EurekaMoviesBE.Dtos.Responses;

namespace EurekaMoviesBE.Features.Commands.UserCommands.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public LoginRequest Payload { get; set; }
    public LoginCommand(LoginRequest payload)
    {
        Payload = payload;
    }
}