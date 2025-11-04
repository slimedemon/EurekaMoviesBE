namespace EurekaMoviesBE.Dtos.Responses;

public class GetWatchListResponse : BaseResponse
{
    public PagingDto Paging { get; set; }
    public List<GetWatchListData> Data { get; set; }
}

public class GetWatchListData
{
    public long WatchListId { get; set; }
    public long TmdbId { get; set; }
    public Movie Movie { get; set; }
}