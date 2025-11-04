namespace EurekaMoviesBE.Features.Queries.MovieQueries.GetMovieDetail;

public class GetMovieDetailHandler : IRequestHandler<GetMovieDetailQuery, GetMovieDetailResponse>
{
    private readonly ITmdbUnitOfWork _mongoUnitOfRepository;
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<GetMovieDetailHandler> _logger;
    private readonly ICustomHttpContextAccessor _customHttpContextAccessor;
    public GetMovieDetailHandler
    (
        ITmdbUnitOfWork mongoUnitOfRepository, 
        ILogger<GetMovieDetailHandler> logger,
        ICustomHttpContextAccessor customHttpContextAccessor,
        IApplicationUnitOfWork unitOfRepository
    )
    {
        _mongoUnitOfRepository = mongoUnitOfRepository;
        _logger = logger;
        _customHttpContextAccessor = customHttpContextAccessor;
        _unitOfRepository = unitOfRepository;
    }
    
    public async Task<GetMovieDetailResponse> Handle(GetMovieDetailQuery request, CancellationToken cancellationToken)
    {
        var tmdbId = request.TmdbId;
        const string functionName = $"{nameof(GetMovieDetailHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetMovieDetailResponse{ Status = (int)ResponseStatusCode.NotFound };

        try
        {   
            var userId = _customHttpContextAccessor.GetCurrentUserId();
            var movie = await _mongoUnitOfRepository.Movie
                .Where(x => x.TmdbId == tmdbId)
                .FirstOrDefaultAsync(cancellationToken);
           
            if (movie is null)
            {
                _logger.LogWarning($"{functionName} Film does not exist");
                response.ErrorMessage = "Film does not exist";
                return response;
            }

            var favorite = await _unitOfRepository.Favorite
                .Where(x => x.UserId == Guid.Parse(userId) && x.TmdbId == tmdbId)
                .FirstOrDefaultAsync(cancellationToken);
            
            var watchlist = await _unitOfRepository.WatchList
                .Where(x => x.UserId == Guid.Parse(userId) && x.TmdbId == tmdbId)
                .FirstOrDefaultAsync(cancellationToken);

            var stars = await _unitOfRepository.Rating
                .Where(x => x.TmdbId == tmdbId)
                .Select(x => x.Star)
                .ToListAsync(cancellationToken);
            var rating = stars.Count > 0 ? stars.Average() : 0.0;
            
            response.Data = new GetMovieDetailData
            {
                Movie = movie,
                FavoriteId = favorite?.Index,
                WatchListId = watchlist?.Index,
                Rating = rating
            };
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