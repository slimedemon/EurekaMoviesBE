namespace EurekaMovieBE.Dtos.Responses;

public class AddMovieToWatchListResponse : BaseResponse
{
    public WatchList Data { get; set; }
}