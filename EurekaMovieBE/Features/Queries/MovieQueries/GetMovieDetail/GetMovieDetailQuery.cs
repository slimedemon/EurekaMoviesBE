namespace EurekaMovieBE.Features.Queries.MovieQueries.GetMovieDetail;

public class GetMovieDetailQuery : IRequest<GetMovieDetailResponse>
{
    public long TmdbId { get; set; }
    public GetMovieDetailQuery(long tmdbId)
    {
        TmdbId = tmdbId;
    }
}