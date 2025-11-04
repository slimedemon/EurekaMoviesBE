namespace EurekaMoviesBE.Features.Queries.MovieQueries.GetTrendingThisWeek;

public class GetTrendingMovieThisWeekQuery : IRequest<GetTrendingMovieThisWeekResponse>
{
    public GetTrendingMovieRequest Payload { get; set; }
    public GetTrendingMovieThisWeekQuery(GetTrendingMovieRequest payload)
    {
        Payload = payload;
    }
}