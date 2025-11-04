namespace EurekaMoviesBE.Dtos.Responses;

public class GetLatestTrailersResponse : BaseResponse
{
    public List<GetLatestTrailersData> Data { get; set; } = new();
    public PagingDto Paging { get; set; }
}

public class GetLatestTrailersData
{
    public Trailer Trailer { get; set; }
    public Movie Movie { get; set; }
}