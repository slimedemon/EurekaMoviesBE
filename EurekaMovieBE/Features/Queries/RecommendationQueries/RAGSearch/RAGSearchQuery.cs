namespace EurekaMovieBE.Features.Queries.RecommendationQueries.RAGSearch;

public class RAGSearchQuery : IRequest<RAGSearchResponse>
{
    public RAGSearchRequest Payload { get; set; }
    public RAGSearchQuery(RAGSearchRequest payload)
    {
        Payload = payload;
    }
}