namespace EurekaMovieBE.Dtos.Requests;

public class GetReviewsRequest
{
    public long TmdbId { get; set; } // movie id
    public int PageNumber { get; set; } = 1;
    public int MaxPerPage { get; set; } = 10;
}