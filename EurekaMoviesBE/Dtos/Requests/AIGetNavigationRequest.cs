namespace EurekaMoviesBE.Dtos.Requests;

public class AIGetNavigationRequest
{
    public string LLMKey { get; set; }
    public string Query { get; set; }
}