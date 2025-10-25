namespace EurekaMovieBE.Dtos.Responses;

public class GetPopularMoviesResponse : BaseResponse
{
    public List<MoviePopular> Data { get; set; } = new();
    public PagingDto Paging { get; set; } = new();
}