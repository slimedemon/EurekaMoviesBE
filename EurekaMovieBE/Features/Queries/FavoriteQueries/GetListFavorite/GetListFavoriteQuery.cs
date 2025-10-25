namespace EurekaMovieBE.Features.Queries.FavoriteQueries.GetListFavorite;

public class GetListFavoriteQuery : IRequest<GetListFavoriteResponse>
{
    public GetListFavoriteRequest Payload { get; set; }
    public GetListFavoriteQuery(GetListFavoriteRequest payload)
    {
        Payload = payload;
    }
}