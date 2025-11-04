namespace EurekaMoviesBE.Dtos.Responses;

public class GetTrendingMovieThisWeekResponse : BaseResponse
{
    public List<MovieTrendingWeek> Data { get; set; } = new();
    public PagingDto Paging { get; set; }
}