namespace EurekaMoviesBE.Features.Queries.RecommendationQueries.RAGSearch;

public class RAGSearchHanlder : IRequestHandler<RAGSearchQuery, RAGSearchResponse>
{
    private readonly IRecommendationService _recommendationService;
    private readonly ILogger<RAGSearchHanlder> _logger;
    private readonly LLMServiceOption _llmServiceOption;
    private readonly ITmdbUnitOfWork _unitOfRepository;
    public RAGSearchHanlder
    (
        IRecommendationService recommendationService,
        ILogger<RAGSearchHanlder> logger,
        IOptions<LLMServiceOption> llmServiceOption,
        ITmdbUnitOfWork unitOfRepository
    )
    {
        _recommendationService = recommendationService;
        _logger = logger;
        _llmServiceOption = llmServiceOption.Value;
        _unitOfRepository = unitOfRepository;
    }

    public async Task<RAGSearchResponse> Handle(RAGSearchQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(RAGSearchHanlder)} =>";
        _logger.LogInformation(functionName);

        var response = new RAGSearchResponse { Status = (int)ResponseStatusCode.Ok };

        try
        {
            var searchRequest = new AILLMRAGSearchRequest
            {
                Query = payload.Query,
                LLMApiKey = _llmServiceOption.LLMApiKey,
                Collection = payload.Collection,
            };

            var searchResponse = await _recommendationService.LLMRAGSearch(searchRequest, cancellationToken);

            if (searchResponse.Status != (int)ResponseStatusCode.Ok)
            {
                response.Status = searchResponse.Status;
                return response;
            }

            response.Data = searchResponse.Data;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = "An error occurred";
            response.Status = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}