namespace EurekaMoviesBE.Dtos.Requests;

public class GetActingListRequest
{
    public string Keyword { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int MaxPerPage { get; set; } = 10;
}
