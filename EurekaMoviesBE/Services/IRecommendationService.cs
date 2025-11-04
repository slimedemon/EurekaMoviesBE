namespace EurekaMoviesBE.Services
{
    public interface IRecommendationService
    {
        Task<AIGetNavigationResponse> GetNavigation(AIGetNavigationRequest request, CancellationToken cancellationToken);
        Task<AILLMSearchResponse> LLMRetrieverSearch(AILLMSearchRequest request, CancellationToken cancellationToken);
        Task<AILLMRAGSearchResponse> LLMRAGSearch(AILLMRAGSearchRequest request, CancellationToken cancellationToken);
    }
}
    