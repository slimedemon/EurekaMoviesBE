namespace EurekaMovieBE.Dtos.Requests;

public class GetTrendingMovieRequest
{
    public int PageNumber { get; set; } = 1;
    public int MaxPerPage { get; set; } = 10;
}