namespace EurekaMoviesBE.Features.Commands.FavoriteCommands.MarkAsFavorite;

public class MarkAsFavoriteCommand : IRequest<MarkAsFavoriteResponse>
{
    public MarkAsFavoriteRequest Payload { get; set; }
    public MarkAsFavoriteCommand(MarkAsFavoriteRequest payload)
    {
        Payload = payload;
    }
}