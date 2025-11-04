namespace EurekaMoviesBE.Features.Queries.CastQueries.GetCastDetail;

public class GetCastDetailHandler : IRequestHandler<GetCastDetailQuery, GetCastDetailResponse>
{
    private readonly ITmdbUnitOfWork _unitOfRepository;
    private readonly ILogger<GetCastDetailHandler> _logger;

    public GetCastDetailHandler
    (
        ITmdbUnitOfWork unitOfRepository, 
        ILogger<GetCastDetailHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task<GetCastDetailResponse> Handle(GetCastDetailQuery request, CancellationToken cancellationToken)
    {
        var tmdbId = request.TmdbId;
        const string functionName = $"{nameof(GetCastDetailHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetCastDetailResponse{ Status = (int)ResponseStatusCode.NotFound };

        try
        {
            var people = await _unitOfRepository.People
                .Where(x => x.TmdbId == tmdbId)
                .FirstOrDefaultAsync(cancellationToken);

            if (people is null)
            {
                _logger.LogWarning($"{functionName} Cast not found");
                response.ErrorMessage = "Cast not found";
                return response;
            }
            
            response.Data = people;
            response.Status = (int)ResponseStatusCode.Ok;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = "An error occurred";
            response.Status = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}