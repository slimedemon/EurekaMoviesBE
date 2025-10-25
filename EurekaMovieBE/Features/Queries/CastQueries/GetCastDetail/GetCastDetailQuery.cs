namespace EurekaMovieBE.Features.Queries.CastQueries.GetCastDetail;

public class GetCastDetailQuery : IRequest<GetCastDetailResponse>
{
    public long TmdbId { get; set; }
    public GetCastDetailQuery(long tmdbId)
    {
        TmdbId = tmdbId;
    }
}