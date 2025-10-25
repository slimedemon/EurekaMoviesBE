namespace EurekaMovieBE.Features.Commands.RatingCommands.AddRating;

public class AddRatingHandler : IRequestHandler<AddRatingCommand, AddRatingResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<AddRatingHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public AddRatingHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<AddRatingHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<AddRatingResponse> Handle(AddRatingCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var payload = request.Payload;
        const string functionName = $"{nameof(AddRatingHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new AddRatingResponse{ Status = (int)ResponseStatusCode.BadRequest };

        try
        { 
           var rating = await _unitOfRepository.Rating
               .Where(f => f.UserId == Guid.Parse(userId) && f.TmdbId == payload.TmdbId)
               .AsNoTracking()
               .FirstOrDefaultAsync(cancellationToken);
           
           if (rating is not null)
           {
               _logger.LogWarning($"{functionName} This user already rating this film");
               response.ErrorMessage = "You are already rating this film";
               return response;
           }
           
           rating = new Rating
           {
               UserId = Guid.Parse(userId),
               TmdbId = payload.TmdbId,
               Star = payload.Stars,
               Comment = payload.Comment,
               CreatedDate = DateTime.UtcNow
           };
           
           await _unitOfRepository.Rating.AddAsync(rating);
           await _unitOfRepository.CompleteAsync();
           
           response.Status = (int)ResponseStatusCode.Created;
           response.Data = rating;
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