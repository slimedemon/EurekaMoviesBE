

namespace EurekaMovieBE.Features.Queries.FavoriteQueries.GetListFavorite;

public class GetListFavoriteHandler : IRequestHandler<GetListFavoriteQuery, GetListFavoriteResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<GetListFavoriteHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    private readonly ITmdbUnitOfWork _mongoUnitOfRepository;
    public GetListFavoriteHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<GetListFavoriteHandler> logger, 
        ICustomHttpContextAccessor httpContextAccessor,
        ITmdbUnitOfWork mongoUnitOfRepository
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _mongoUnitOfRepository = mongoUnitOfRepository;
    }
    public async Task<GetListFavoriteResponse> Handle(GetListFavoriteQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var payload = request.Payload;
        const string functionName = $"{nameof(GetListFavoriteHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetListFavoriteResponse{ Status = (int)ResponseStatusCode.Ok };

        try
        { 
            var pagination = await _unitOfRepository.Favorite
                .Where(f => f.UserId == Guid.Parse(userId))
                .OrderBy(f => f.Index)
                .Select(x => new GetListFavoriteData
                {
                    FavoriteId = x.Index,
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

            foreach (var favoriteData in data)
            {
                favoriteData.Movie = movies.FirstOrDefault(x => x.TmdbId == favoriteData.TmdbId);
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