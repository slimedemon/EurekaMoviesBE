namespace EurekaMovieBE.Dtos.Requests;

public class AILLMRAGSearchRequest
{
    public string LLMApiKey { get; set; }
    public string Collection { get; set; }
    public string Query { get; set; }
}