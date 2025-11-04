namespace EurekaMoviesBE.Features.Queries.MovieQueries.GetMovieDetailById;

public class GetMovieDetailHandler : IRequestHandler<GetMovieDetailByIdQuery, GetMovieDetailByIdResponse>
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
    
    public async Task<GetMovieDetailByIdResponse> Handle(GetMovieDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        const string functionName = $"{nameof(GetMovieDetailHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetMovieDetailByIdResponse{ Status = (int)ResponseStatusCode.NotFound };

        try
        {   
            var userId = _customHttpContextAccessor.GetCurrentUserId();
            var movie = await _mongoUnitOfRepository.Movie
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
           
            if (movie is null)
            {
                _logger.LogWarning($"{functionName} Film does not exist");
                response.ErrorMessage = "Film does not exist";
                return response;
            }

            var favorite = await _unitOfRepository.Favorite
                .Where(x => x.UserId == Guid.Parse(userId) && x.TmdbId == movie.TmdbId)
                .FirstOrDefaultAsync(cancellationToken);
            
            var watchlist = await _unitOfRepository.WatchList
                .Where(x => x.UserId == Guid.Parse(userId) && x.TmdbId == movie.TmdbId)
                .FirstOrDefaultAsync(cancellationToken);

            var stars = await _unitOfRepository.Rating
                .Where(x => x.TmdbId == movie.TmdbId)
                .Select(x => x.Star)
                .ToListAsync(cancellationToken);
            var rating = stars.Count > 0 ? stars.Average() : 0.0;
            
            response.Data = new GetMovieDetailByIdData
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