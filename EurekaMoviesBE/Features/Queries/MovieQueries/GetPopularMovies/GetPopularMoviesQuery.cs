namespace EurekaMoviesBE.Features.Queries.MovieQueries.GetPopularMovies;

public class GetPopularMoviesQuery : IRequest<GetPopularMoviesResponse>
{
    public GetPopularMovieRequest Payload { get; set; }
    public GetPopularMoviesQuery(GetPopularMovieRequest payload)
    {
        Payload = payload;
    }
}