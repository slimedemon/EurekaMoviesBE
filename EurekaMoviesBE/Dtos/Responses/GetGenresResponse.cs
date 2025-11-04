namespace EurekaMoviesBE.Dtos.Responses;

public class GetGenresResponse : BaseResponse
{
    public List<MovieGenre> Data { get; set; }
}