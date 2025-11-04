namespace EurekaMoviesBE.Dtos.Responses;

public class MarkAsFavoriteResponse : BaseResponse
{
    public Favorite Data { get; set; }
}