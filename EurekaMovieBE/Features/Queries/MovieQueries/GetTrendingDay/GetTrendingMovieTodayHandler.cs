namespace EurekaMovieBE.Features.Queries.MovieQueries.GetTrendingDay;

public class GetTrendingMovieTodayHandler : IRequestHandler<GetTrendingMovieTodayQuery, GetTrendingMovieTodayResponse>
{
    private readonly ITmdbUnitOfWork _unitOfRepository;
    private readonly ILogger<GetTrendingMovieTodayHandler> _logger;

    public GetTrendingMovieTodayHandler
    (
        ITmdbUnitOfWork unitOfRepository, 
        ILogger<GetTrendingMovieTodayHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task<GetTrendingMovieTodayResponse> Handle(GetTrendingMovieTodayQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(GetTrendingMovieTodayHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetTrendingMovieTodayResponse{ Status = (int)ResponseStatusCode.Ok };

        try
        {
            var movies = await _unitOfRepository.MovieTrendingDay
                .GetAll()
                .OrderByDescending(x => x.VoteAverage)
                .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);
           
            response.Data = movies.Data;
            response.Paging = movies.Paging;
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