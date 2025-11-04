namespace EurekaMoviesBE.Features.Queries.RecommendationQueries.LLMSearch;

public class LLMSearchQuery : IRequest<LLMSearchResponse>
{
    public LLMSearchRequest Payload { get; set; }
    public LLMSearchQuery(LLMSearchRequest payload)
    {
        Payload = payload;
    }
}