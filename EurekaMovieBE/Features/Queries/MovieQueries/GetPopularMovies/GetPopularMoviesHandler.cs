namespace EurekaMovieBE.Features.Queries.MovieQueries.GetPopularMovies;

public class GetPopularMoviesHandler : IRequestHandler<GetPopularMoviesQuery, GetPopularMoviesResponse>
{
    private readonly ITmdbUnitOfWork _unitOfRepository;
    private readonly ILogger<GetPopularMoviesHandler> _logger;

    public GetPopularMoviesHandler
    (
        ITmdbUnitOfWork unitOfRepository, 
        ILogger<GetPopularMoviesHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }

    public async Task<GetPopularMoviesResponse> Handle(GetPopularMoviesQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(GetPopularMoviesHandler)} =>";
        _logger.LogInformation(functionName);

        var response = new GetPopularMoviesResponse { Status = (int)ResponseStatusCode.Ok };

        try
        {
            var pagination = await _unitOfRepository.MoviePopular
                .GetAll()
                .Sort(Builders<MoviePopular>.Sort.Descending(x => x.Popularity))
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