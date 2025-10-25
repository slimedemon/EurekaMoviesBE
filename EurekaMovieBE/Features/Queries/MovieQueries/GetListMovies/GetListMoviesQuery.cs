namespace EurekaMovieBE.Features.Queries.MovieQueries.GetListMovies;

public class GetListMoviesQuery : IRequest<GetListMoviesResponse>
{
    public GetListMoviesRequest Payload { get; set; }
    public GetListMoviesQuery(GetListMoviesRequest payload)
    {
        Payload = payload;
    }
}