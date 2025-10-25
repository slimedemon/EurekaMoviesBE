namespace EurekaMovieBE.Features.Queries.RecommendationQueries.GetNavigation;

//public class GetNavigationHandler : IRequestHandler<GetNavigationQuery, GetNavigationResponse>
//{
//    private readonly IRecommendationService _recommendationService;
//    private readonly ILogger<GetNavigationHandler> _logger;
//    private readonly LLMServiceOption _llmServiceOption;
//    public GetNavigationHandler
//        (
//            IRecommendationService recommendationService, 
//            ILogger<GetNavigationHandler> logger,
//            IOptions<LLMServiceOption> llmServiceOption
//        )
//    {
//        _recommendationService = recommendationService;
//        _logger = logger;
//        _llmServiceOption = llmServiceOption.Value;
//    }
    
//    public async Task<GetNavigationResponse> Handle(GetNavigationQuery request, CancellationToken cancellationToken)
//    {
//        var query = request.Query;
//        const string functionName = $"{nameof(GetNavigationHandler)} =>";
//        _logger.LogInformation(functionName);
        
//        var response = new GetNavigationResponse{ Status = (int)ResponseStatusCode.Ok };

//        try
//        {
//            var getNavigationRequest = new AIGetNavigationRequest
//            {
//                Query = query,
//                LLMKey = _llmServiceOption.LLMApiKey
//            };
            
//            var getNavigationResponse = await _recommendationService.GetNavigation(getNavigationRequest, cancellationToken);

//            if (getNavigationResponse.Status != (int)ResponseStatusCode.Ok)
//            {
//                response.Status = getNavigationResponse.Status;
//                return response;
//            }

//            var data = getNavigationResponse.Data;
            
//            var responseData = new GetNavigationData
//            {
//                Params = data.Params,
//                Route = data.Route,
//                IsSuccess = data.IsSuccess
//            };
            
//            if (data.Metadata is null)
//            {
//                responseData.Metadata = null;
//            }
//            else
//            {
//                responseData.Metadata = data.Metadata.ToString();
//            }
            
//            response.Data = responseData;

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