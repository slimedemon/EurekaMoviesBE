namespace EurekaMovieBE.Dtos.Responses;

public class LLMSearchResponse : BaseResponse
{
    public List<Movie> Data { get; set; }
}