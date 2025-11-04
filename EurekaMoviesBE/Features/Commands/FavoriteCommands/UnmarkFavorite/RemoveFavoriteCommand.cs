namespace EurekaMoviesBE.Features.Commands.FavoriteCommands.UnmarkFavorite;

public class RemoveFavoriteCommand : IRequest<UnmarkFavoriteResponse>
{
    public long FavoriteId { get; set; }

    public RemoveFavoriteCommand(long favoriteId)
    {
        FavoriteId = favoriteId;
    }
}