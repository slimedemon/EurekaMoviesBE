using MediatR;
using EurekaMovieBE.Dtos.Requests;
using EurekaMovieBE.Dtos.Responses;

namespace EurekaMovieBE.Features.Commands.WatchListCommands.AddMovieToWatchList;

public class AddMovieToWatchListCommand : IRequest<AddMovieToWatchListResponse>
{
    public AddMovieToWatchListRequest Payload{ get; set; }
    public AddMovieToWatchListCommand(AddMovieToWatchListRequest payload)
    {
        Payload = payload;
    }
}