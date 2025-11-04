namespace EurekaMoviesBE.Features.Commands.WatchListCommands.RemoveMovieFromWatchList;

public class RemoveMovieFromWatchListCommand : IRequest<RemoveMovieFromWatchListResponse>
{
    public long Id { get; set; }
    public RemoveMovieFromWatchListCommand(long id)
    {
        Id = id;
    }
}