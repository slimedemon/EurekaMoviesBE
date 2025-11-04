namespace EurekaMoviesBE.Dtos.Responses;

public class AddMovieToWatchListResponse : BaseResponse
{
    public WatchList Data { get; set; }
}