namespace EurekaMovieBE.Dtos.Responses;

public class GetMovieDetailResponse : BaseResponse
{
    public GetMovieDetailData Data { get; set; }
}

public class GetMovieDetailData
{
    public Movie Movie { get; set; }
    public long? WatchListId { get; set; }
    public long? FavoriteId { get; set; }
    public double Rating { get; set; }
}
