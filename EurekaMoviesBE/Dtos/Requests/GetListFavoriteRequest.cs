namespace EurekaMoviesBE.Dtos.Requests;

public class GetListFavoriteRequest
{
    public int PageNumber { get; set; } = 1;
    public int MaxPerPage { get; set; } = 10;
}