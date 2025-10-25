namespace EurekaMovieBE.Dtos.Responses;

public class GetTrendingMovieTodayResponse : BaseResponse
{
    public List<MovieTrendingDay> Data { get; set; } = new();
    public PagingDto Paging { get; set; }
}