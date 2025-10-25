namespace EurekaMovieBE.Dtos.Responses;

public class GetListMoviesResponse : BaseResponse
{
    public List<Movie> Data { get; set; }
    public PagingDto Paging { get; set; }
}