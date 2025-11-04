namespace EurekaMoviesBE.Features.Commands.RatingCommands.DeleteRating;

public class DeleteRatingHandler : IRequestHandler<DeleteRatingCommand, DeleteRatingResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<DeleteRatingHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public DeleteRatingHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<DeleteRatingHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<DeleteRatingResponse> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var ratingId = request.Id;
        const string functionName = $"{nameof(DeleteRatingHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new DeleteRatingResponse{ Status = (int)ResponseStatusCode.NotFound };

        try
        { 
            var rating = await _unitOfRepository.Rating
                .Where(f => f.Index == ratingId && f.UserId == Guid.Parse(userId))
                .FirstOrDefaultAsync(cancellationToken);
           
            if (rating is null)
            {
                _logger.LogWarning($"{functionName} Rating not found");
                response.ErrorMessage = "Rating not found";
                return response;
            }
           
           
            _unitOfRepository.Rating.Delete(rating);
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