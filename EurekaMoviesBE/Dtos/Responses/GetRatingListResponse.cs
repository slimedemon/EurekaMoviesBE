namespace EurekaMoviesBE.Dtos.Responses;

public class GetRatingListResponse : BaseResponse
{
    public List<GetRatingListData> Data { get; set; }
    public PagingDto Paging { get; set; }
}

public class GetRatingListData
{
    public long Id { get; set; }
    public string Comment { get; set; }
    public double Stars { get; set; }
    public DateTime CreatedDate { get; set; }
    public long TmdbId { get; set; }
    public Movie? Movie { get; set; }
}