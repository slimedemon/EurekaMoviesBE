namespace EurekaMovieBE.Features.Commands.WatchListCommands.AddMovieToWatchList;

public class AddMovieToWatchListHandler : IRequestHandler<AddMovieToWatchListCommand, AddMovieToWatchListResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<AddMovieToWatchListHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public AddMovieToWatchListHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<AddMovieToWatchListHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<AddMovieToWatchListResponse> Handle(AddMovieToWatchListCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var tmdbId = request.Payload.TmdbId;
        const string functionName = $"{nameof(AddMovieToWatchListHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new AddMovieToWatchListResponse{ Status = (int)ResponseStatusCode.BadRequest };

        try
        { 
           var watchList = await _unitOfRepository.WatchList
               .Where(f => f.UserId == Guid.Parse(userId) && f.TmdbId == tmdbId)
               .AsNoTracking()
               .FirstOrDefaultAsync(cancellationToken);
           
           if (watchList is not null)
           {
               _logger.LogWarning($"{functionName} Film already in watch list");
               response.ErrorMessage = "Film already in watch list";
               return response;
           }
           
           watchList = new WatchList
           {
               UserId = Guid.Parse(userId),
               TmdbId = tmdbId,
               CreatedDate = DateTime.UtcNow
           };
           
           await _unitOfRepository.WatchList.AddAsync(watchList);
           await _unitOfRepository.CompleteAsync();
           
           response.Status = (int)ResponseStatusCode.Created;
           response.Data = watchList;
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