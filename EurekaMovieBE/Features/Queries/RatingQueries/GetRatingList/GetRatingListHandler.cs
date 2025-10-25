namespace EurekaMovieBE.Features.Queries.RatingQueries.GetRatingList;

public class GetReviewsHandler : IRequestHandler<GetReviewsQuery, GetReviewsResponse>
{
    private readonly IApplicationUnitOfWork _unitOfRepository;
    private readonly ILogger<GetReviewsHandler> _logger;
    public GetReviewsHandler
    (
        IApplicationUnitOfWork unitOfRepository, 
        ILogger<GetReviewsHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger; ;
    }
    public async Task<GetReviewsResponse> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(GetReviewsHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetReviewsResponse{ Status = (int)ResponseStatusCode.Ok };

        try
        { 
            var pagination = await 
            (
                from rating in _unitOfRepository.Rating.GetAll()
                join user in _unitOfRepository.UserInfo.GetAll()
                    on rating.UserId equals user.Id
                where rating.TmdbId ==  payload.TmdbId
                orderby rating.CreatedDate descending
                select new GetReviewsData
                {
                    Id = rating.Index,
                    Comment = rating.Comment,
                    CreatedDate = rating.CreatedDate,
                    Stars = rating.Star,
                    UserAvatar = user.Avatar,
                    UserName = user.DisplayName
                }
            )
            .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);
            
            response.Paging = pagination.Paging;
            response.Data = pagination.Data;
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