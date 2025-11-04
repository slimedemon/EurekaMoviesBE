namespace EurekaMoviesBE.Features.Commands.WatchListCommands.AddMovieToWatchList;

public class AddMovieToWatchListCommand : IRequest<AddMovieToWatchListResponse>
{
    public AddMovieToWatchListRequest Payload{ get; set; }
    public AddMovieToWatchListCommand(AddMovieToWatchListRequest payload)
    {
        Payload = payload;
    }
}