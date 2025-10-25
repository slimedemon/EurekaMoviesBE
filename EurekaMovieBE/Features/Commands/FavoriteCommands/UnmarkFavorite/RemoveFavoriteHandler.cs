namespace EurekaMovieBE.Features.Commands.FavoriteCommands.UnmarkFavorite;

public class RemoveFavoriteHandler : IRequestHandler<RemoveFavoriteCommand, UnmarkFavoriteResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<RemoveFavoriteHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public RemoveFavoriteHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<RemoveFavoriteHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<UnmarkFavoriteResponse> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var favoriteId = request.FavoriteId;
        const string functionName = $"{nameof(RemoveFavoriteHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new UnmarkFavoriteResponse{ Status = (int)ResponseStatusCode.NotFound };

        try
        { 
           var favorite = await _unitOfRepository.Favorite
               .Where(f => f.Index == favoriteId && f.UserId == Guid.Parse(userId))
               .FirstOrDefaultAsync(cancellationToken);
           
           if (favorite is null)
           {
               _logger.LogWarning($"{functionName} Favorite does not exist");
               response.ErrorMessage = "Favorite does not exist";
               return response;
           }
           
           
           _unitOfRepository.Favorite.Delete(favorite);
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