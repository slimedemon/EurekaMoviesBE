namespace EurekaMovieBE.Features.Queries.RecommendationQueries.LLMSearch;

//public class LLMSearchHanlder : IRequestHandler<LLMSearchQuery, LLMSearchResponse>
//{
//    private readonly IRecommendationService _recommendationService;
//    private readonly ILogger<LLMSearchHanlder> _logger;
//    private readonly LLMServiceOption _llmServiceOption;
//    private readonly IMongoUnitOfRepository _unitOfRepository;
//    public LLMSearchHanlder
//    (
//        IRecommendationService recommendationService, 
//        ILogger<LLMSearchHanlder> logger,
//        IOptions<LLMServiceOption> llmServiceOption,
//        IMongoUnitOfRepository unitOfRepository
//    )
//    {
//        _recommendationService = recommendationService;
//        _logger = logger;
//        _llmServiceOption = llmServiceOption.Value;
//        _unitOfRepository = unitOfRepository;
//    }
    
//    public async Task<LLMSearchResponse> Handle(LLMSearchQuery request, CancellationToken cancellationToken)
//    {
//        var payload = request.Payload;
//        const string functionName = $"{nameof(LLMSearchHanlder)} =>";
//        _logger.LogInformation(functionName);
        
//        var response = new LLMSearchResponse{ Status = (int)ResponseStatusCode.Ok };

//        try
//        {
//            var searchRequest = new AILLMSearchRequest
//            {
//                Query = payload.Query,
//                LLMApiKey = _llmServiceOption.LLMApiKey,
//                Amount = payload.Amount,
//                Collection = payload.Collection,
//                Threshold = payload.Threshold
//            };
            
//            var searchResponse = await _recommendationService.LLMRetrieverSearch(searchRequest, cancellationToken);

//            if (searchResponse.Status != (int)ResponseStatusCode.Ok)
//            {
//                response.Status = searchResponse.Status;
//                return response;
//            }

//            var movies = await _unitOfRepository.Movie
//                .Where(x => searchResponse.Data.Result.Contains(x.Id))
//                .ToListAsync(cancellationToken);
            
//            response.Data = movies;

//        }
//        catch(Exception ex)
//        {
//            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
//            response.ErrorMessage = "An error occurred";
//            response.Status = (int)ResponseStatusCode.InternalServerError;
//        }

//        return response;
//    }

//    #region Private methods
    

//    #endregion
//}