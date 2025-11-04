namespace EurekaMoviesBE.Dtos.Requests;

public class GetRatingListRequest
{
    public int PageNumber { get; set; } = 1;
    public int MaxPerPage { get; set; } = 10;
}