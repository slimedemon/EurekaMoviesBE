namespace EurekaMoviesBE.Features.Queries.RatingQueries.GetReviews;

public class GetRatingListHandler : IRequestHandler<GetRatingListQuery, GetRatingListResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ITmdbUnitOfWork _mongoUnitOfRepository;
    private readonly ILogger<GetRatingListHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetRatingListHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<GetRatingListHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor,
        ITmdbUnitOfWork mongoUnitOfRepository
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _mongoUnitOfRepository = mongoUnitOfRepository;
    }
    
    public async Task<GetRatingListResponse> Handle(GetRatingListQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _httpContextAccessor.GetCurrentUserId();
        var payload = request.Payload;
        const string functionName = $"{nameof(GetRatingListHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetRatingListResponse{ Status = (int)ResponseStatusCode.Ok };

        try
        { 
            var pagination = await _unitOfRepository.Rating
                .Where(x => x.UserId == Guid.Parse(currentUserId))
                .Select(x => new GetRatingListData
                {
                    Id = x.Index,
                    Comment = x.Comment,
                    CreatedDate = x.CreatedDate,
                    Stars = x.Star
                })
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);
            
            var ratingItems = pagination.Data;
            var moviesIds = ratingItems.Select(x => x.TmdbId).ToList();
            
            var movies = _mongoUnitOfRepository.Movie
                .Where(x => moviesIds.Contains(x.TmdbId))
                .ToListAsync(cancellationToken);
            
            foreach (var ratingItem in ratingItems)
            {
                ratingItem.Movie = movies.Result.FirstOrDefault(x => x.TmdbId == ratingItem.TmdbId);
            }
            
            response.Paging = pagination.Paging;
            response.Data = pagination.Data;
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