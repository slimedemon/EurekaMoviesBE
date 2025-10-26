namespace EurekaMovieBE.Features.Commands.WatchListCommands.RemoveMovieFromWatchList;

public class RemoveMovieFromWatchListHandler : IRequestHandler<RemoveMovieFromWatchListCommand, RemoveMovieFromWatchListResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<RemoveMovieFromWatchListHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public RemoveMovieFromWatchListHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<RemoveMovieFromWatchListHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<RemoveMovieFromWatchListResponse> Handle(RemoveMovieFromWatchListCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var watchListId = request.Id;
        const string functionName = $"{nameof(RemoveMovieFromWatchListHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new RemoveMovieFromWatchListResponse{ Status = (int)ResponseStatusCode.NotFound };

        try
        { 
            var watchList = await _unitOfRepository.WatchList
                .Where(f => f.Index == watchListId && f.UserId == Guid.Parse(userId))
                .FirstOrDefaultAsync(cancellationToken);
           
            if (watchList is null)
            {
                _logger.LogWarning($"{functionName}  Film doesn't exists in watch list");
                response.ErrorMessage = "Film doesn't exists in watch list";
                return response;
            }
           
           
            _unitOfRepository.WatchList.Delete(watchList);
            await _unitOfRepository.CompleteAsync();
           
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