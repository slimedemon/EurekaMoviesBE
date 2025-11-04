namespace EurekaMoviesBE.Features.Commands.RatingCommands.EditRating;

public class EditRatingHandler : IRequestHandler<EditRatingCommand, EditRatingResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<EditRatingHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public EditRatingHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<EditRatingHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<EditRatingResponse> Handle(EditRatingCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var payload = request.Payload;
        const string functionName = $"{nameof(EditRatingHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new EditRatingResponse{ Status = (int)ResponseStatusCode.NotFound };

        try
        { 
           var rating = await _unitOfRepository.Rating
               .Where(x => x.UserId == Guid.Parse(userId) && x.Index == payload.RatingId)
               .FirstOrDefaultAsync(cancellationToken);
           
           if (rating is null)
           {
               _logger.LogWarning($"{functionName} Rating not found");
               response.ErrorMessage = "Rating not found";
               return response;
           }
           
           rating.Star = payload.Stars;
           rating.Comment = payload.Comment;
           
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