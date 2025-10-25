namespace EurekaMovieBE.Dtos.Requests;

public class AILLMSearchRequest
{
    public string LLMApiKey { get; set; }
    public string Collection { get; set; }
    public string Query { get; set; }
    public double Amount { get; set; } = 10;
    public double Threshold { get; set; } = 0.25;
}