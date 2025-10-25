namespace EurekaMovieBE.Dtos.Responses;

public class GetGenresResponse : BaseResponse
{
    public List<MovieGenre> Data { get; set; }
}