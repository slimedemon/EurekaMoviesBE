using MediatR;
using EurekaMovieBE.Dtos.Requests;
using EurekaMovieBE.Dtos.Responses;

namespace EurekaMovieBE.Features.Commands.UserCommands.RenewPassword;

public class RenewPasswordCommand : IRequest<RenewPasswordResponse>
{
    public RenewPasswordRequest Payload { get; set; }
    public RenewPasswordCommand(RenewPasswordRequest payload)
    {
        Payload = payload;
    }
}