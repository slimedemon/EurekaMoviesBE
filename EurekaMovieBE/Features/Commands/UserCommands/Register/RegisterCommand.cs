using MediatR;
using EurekaMovieBE.Dtos.Requests;
using EurekaMovieBE.Dtos.Responses;

namespace EurekaMovieBE.Features.Commands.UserCommands.Register;

public class RegisterCommand : IRequest<RegisterResponse>
{
    public RegisterRequest Payload { get; set; }
    public bool IsSocial { get; set; } = false;
    public RegisterCommand(RegisterRequest payload)
    {
        Payload = payload;
    }
}