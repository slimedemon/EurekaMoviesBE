namespace EurekaMoviesBE.Dtos.Responses;

public class GetMovieDetailByIdResponse : BaseResponse
{
    public GetMovieDetailByIdData Data { get; set; }
}

public class GetMovieDetailByIdData
{
    public Movie Movie { get; set; }
    public long? WatchListId { get; set; }
    public long? FavoriteId { get; set; }
    public double Rating { get; set; }
}
