namespace EurekaMoviesBE.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly ILogger<RecommendationService> _logger;
        private readonly IHttpClientCustom _httpClient;
        private readonly LLMServiceOption _llmServiceOption;
        public RecommendationService
        (
            ILogger<RecommendationService> logger,
            IHttpClientCustom httpClient, IOptions<LLMServiceOption> llmServiceOption)
        {
            _logger = logger;
            _httpClient = httpClient;
            _llmServiceOption = llmServiceOption.Value;
        }

        public async Task<AIGetNavigationResponse> GetNavigation(AIGetNavigationRequest request, CancellationToken cancellationToken)
        {
            var result = new AIGetNavigationResponse { Status = (int)ResponseStatusCode.InternalServerError };
            const string functionName = $"{nameof(RecommendationService)} {nameof(GetNavigation)} =>";

            try
            {
                // var content = JsonHelper.Serialize(request);
                var path = $"{_llmServiceOption.AiNavigationPath}?llm_api_key={request.LLMKey}&query={request.Query}";
                var response = await _httpClient.PostAsync<AIGetNavigationResponse>(_llmServiceOption.AiHost, path, "", cancellationToken);
                return response ?? result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
                return result;
            }
        }

        public async Task<AILLMSearchResponse> LLMRetrieverSearch(AILLMSearchRequest request, CancellationToken cancellationToken)
        {
            var result = new AILLMSearchResponse { Status = (int)ResponseStatusCode.InternalServerError };
            const string functionName = $"{nameof(RecommendationService)} {nameof(GetNavigation)} =>";

            try
            {
                // var content = JsonHelper.Serialize(request);
                var path =
                    $"{_llmServiceOption.LlmRetrieverPath}?llm_api_key={request.LLMApiKey}&collection_name={request.Collection}&query={request.Query}&amount={request.Amount}&threshold={request.Threshold}";
                var response =
                    await _httpClient.GetAsync<AILLMSearchResponse>(_llmServiceOption.AiHost, path, cancellationToken);
                return response ?? result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
                return result;
            }
        }

        public async Task<AILLMRAGSearchResponse> LLMRAGSearch(AILLMRAGSearchRequest request, CancellationToken cancellationToken)
        {
            var result = new AILLMRAGSearchResponse { Status = (int)ResponseStatusCode.InternalServerError };
            const string functionName = $"{nameof(RecommendationService)} {nameof(LLMRAGSearch)} =>";

            try
            {
                // var content = JsonHelper.Serialize(request);
                var path =
                    $"{_llmServiceOption.LlmRagPath}?llm_api_key={request.LLMApiKey}&collection_name={request.Collection}&query={request.Query}";
                var response =
                    await _httpClient.PostAsync<AILLMRAGSearchResponse>(_llmServiceOption.AiHost, path, cancellationToken);
                return response ?? result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
                return result;
            }
        }
    }
}