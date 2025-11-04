namespace EurekaMoviesBE.Features.Queries.CastQueries.GetActingList;

public class GetActingListHandler : IRequestHandler<GetActingListQuery, GetActingListResponse>
{
    private readonly ITmdbUnitOfWork _unitOfRepository;
    private readonly ILogger<GetActingListHandler> _logger;

    public GetActingListHandler
    (
        ITmdbUnitOfWork unitOfRepository, 
        ILogger<GetActingListHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }

    public async Task<GetActingListResponse> Handle(GetActingListQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(GetActingListHandler)} =>";
        _logger.LogInformation(functionName);

        var response = new GetActingListResponse { Status = (int)ResponseStatusCode.Ok };

        try
        {
            var keyword = payload.Keyword.Trim().ToLower();
            var pagination = await _unitOfRepository.People
                .Where(x =>
                    (string.IsNullOrEmpty(keyword)
                    || x.Name.ToLower().Contains(keyword))
                    && x.KnownForDepartment == CastDepartmentConstants.Acting
                )
                .ToListAsPageAsync(payload.PageNumber, payload.MaxPerPage, cancellationToken);

            response.Data = pagination.Data;
            response.Paging = pagination.Paging;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = "An error occurred";
            response.Status = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}