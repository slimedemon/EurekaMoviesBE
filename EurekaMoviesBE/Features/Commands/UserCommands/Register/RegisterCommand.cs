using MediatR;
using EurekaMoviesBE.Dtos.Requests;
using EurekaMoviesBE.Dtos.Responses;

namespace EurekaMoviesBE.Features.Commands.UserCommands.Register;

public class RegisterCommand : IRequest<RegisterResponse>
{
    public RegisterRequest Payload { get; set; }
    public bool IsSocial { get; set; } = false;
    public RegisterCommand(RegisterRequest payload)
    {
        Payload = payload;
    }
}