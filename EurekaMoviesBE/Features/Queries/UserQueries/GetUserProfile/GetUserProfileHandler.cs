namespace EurekaMoviesBE.Features.Queries.UserQueries.GetUserProfile;

public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<GetUserProfileHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public GetUserProfileHandler(IApplicationUnitOfWork unitOfRepository, ILogger<GetUserProfileHandler> logger , ICustomHttpContextAccessor httpContextAccessor)
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _httpContextAccessor.GetCurrentUserId();
        string functionName = $"{nameof(GetUserProfileHandler)} UserId = {currentUserId} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetUserProfileResponse{ Status = (int)ResponseStatusCode.Ok };

        try
        {
            var user = await _unitOfRepository.UserInfo
                .Where(x => x.Id == Guid.Parse(currentUserId))
                .Select(x => new GetUserProfileData
                {
                    UserId = x.Id.ToString(),
                    Avatar = x.Avatar,
                    Email = x.Email,
                    DisplayName = x.DisplayName
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                response.Status = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = "User not found";
                return response;
            }
            
            response.Data = user;
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