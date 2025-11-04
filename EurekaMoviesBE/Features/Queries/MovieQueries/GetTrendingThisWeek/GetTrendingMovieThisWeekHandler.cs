namespace EurekaMoviesBE.Features.Queries.MovieQueries.GetTrendingThisWeek;

public class GetTrendingMovieThisWeekHandler : IRequestHandler<GetTrendingMovieThisWeekQuery, GetTrendingMovieThisWeekResponse>
{
    private readonly ITmdbUnitOfWork _unitOfRepository;
    private readonly ILogger<GetTrendingMovieThisWeekHandler> _logger;

    public GetTrendingMovieThisWeekHandler
    (
        ITmdbUnitOfWork unitOfRepository, 
        ILogger<GetTrendingMovieThisWeekHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }

    public async Task<GetTrendingMovieThisWeekResponse> Handle(GetTrendingMovieThisWeekQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(GetTrendingMovieThisWeekHandler)} =>";
        _logger.LogInformation(functionName);

        var response = new GetTrendingMovieThisWeekResponse { Status = (int)ResponseStatusCode.Ok };

        try
        {
            var pagination = await _unitOfRepository.MovieTrendingWeek
                .GetAll()
                .Sort(Builders<MovieTrendingWeek>.Sort.Descending(x => x.VoteAverage))
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