namespace EurekaMoviesBE.Dtos.Requests;

public class GetListMoviesRequest
{
    public string Keyword { get; set; } = string.Empty;
    public int? GenreId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int MaxPerPage { get; set; } = 10;
}