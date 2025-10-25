namespace EurekaMovieBE.Dtos.Responses;

public class AILLMSearchResponse : AIBaseResponse<AILLMSearchData>
{
    
}

public class AILLMSearchData
{
    public List<string> Result { get; set; }
}