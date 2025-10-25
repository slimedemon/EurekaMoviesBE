namespace EurekaMovieBE.Features.Queries.WatchListQueries.GetWatchList;

public class GetWatchListHandler : IRequestHandler<GetWatchListQuery, GetWatchListResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<GetWatchListHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    private readonly ITmdbUnitOfWork _mongoUnitOfRepository;
    public GetWatchListHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<GetWatchListHandler> logger, 
        ICustomHttpContextAccessor httpContextAccessor,
        ITmdbUnitOfWork mongoUnitOfRepository
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _mongoUnitOfRepository = mongoUnitOfRepository;
    }
    public async Task<GetWatchListResponse> Handle(GetWatchListQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var payload = request.Payload;
        const string functionName = $"{nameof(GetWatchListHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetWatchListResponse{ Status = (int)ResponseStatusCode.Ok };

        try
        { 
            var pagination = await _unitOfRepository.WatchList
                .Where(f => f.UserId == Guid.Parse(userId))
                .OrderBy(f => f.Index)
                .Select(x => new GetWatchListData
                {
                    WatchListId = x.Index,
                    TmdbId = x.TmdbId
                })
                .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);
            
            response.Paging = pagination.Paging;
            
            var data = pagination.Data;
            if (!data.Any())
            {
                return response;
            }
            
            var tmdbIds = data
                .Select(x => x.TmdbId)
                .ToList();

            var movies = await _mongoUnitOfRepository.Movie
                .Where(x => tmdbIds.Contains(x.TmdbId))
                .ToListAsync(cancellationToken);

            foreach (var watchListData in data)
            {
                watchListData.Movie = movies.FirstOrDefault(x => x.TmdbId == watchListData.TmdbId);
            }
            
            response.Status = (int)ResponseStatusCode.Created;
            response.Data = data;
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