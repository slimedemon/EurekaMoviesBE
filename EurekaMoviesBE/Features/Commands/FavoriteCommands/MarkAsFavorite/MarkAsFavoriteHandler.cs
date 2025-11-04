namespace EurekaMoviesBE.Features.Commands.FavoriteCommands.MarkAsFavorite;

public class MarkAsFavoriteHandler : IRequestHandler<MarkAsFavoriteCommand, MarkAsFavoriteResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<MarkAsFavoriteHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public MarkAsFavoriteHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<MarkAsFavoriteHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<MarkAsFavoriteResponse> Handle(MarkAsFavoriteCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var tmdbId = request.Payload.TmdbId;
        const string functionName = $"{nameof(MarkAsFavoriteHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new MarkAsFavoriteResponse{ Status = (int)ResponseStatusCode.BadRequest };

        try
        { 
           var favorite = await _unitOfRepository.Favorite
               .Where(f => f.UserId == Guid.Parse(userId) && f.TmdbId == tmdbId)
               .AsNoTracking()
               .FirstOrDefaultAsync(cancellationToken);
           
           if (favorite is not null)
           {
               _logger.LogWarning($"{functionName} Film already in favorite");
               response.ErrorMessage = "Film already in favorite";
               return response;
           }
           
           favorite = new Favorite
           {
               UserId = Guid.Parse(userId),
               TmdbId = tmdbId,
               CreatedDate = DateTime.UtcNow
           };
           
           await _unitOfRepository.Favorite.AddAsync(favorite);
           await _unitOfRepository.CompleteAsync();
           
           response.Status = (int)ResponseStatusCode.Created;
           response.Data = favorite;
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