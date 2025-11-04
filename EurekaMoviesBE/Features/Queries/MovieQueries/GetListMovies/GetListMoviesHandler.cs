namespace EurekaMoviesBE.Features.Queries.MovieQueries.GetListMovies;

public class GetListMoviesHandler : IRequestHandler<GetListMoviesQuery, GetListMoviesResponse>
{
    private readonly ITmdbUnitOfWork _unitOfRepository;
    private readonly ILogger<GetListMoviesHandler> _logger;

    public GetListMoviesHandler
    (
        ITmdbUnitOfWork unitOfRepository, 
        ILogger<GetListMoviesHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }

    public async Task<GetListMoviesResponse> Handle(GetListMoviesQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(GetListMoviesHandler)} =>";
        _logger.LogInformation(functionName);

        var response = new GetListMoviesResponse { Status = (int)ResponseStatusCode.Ok };

        try
        {
            var keyword = payload.Keyword.Trim().ToLower();
            var pagination = await _unitOfRepository.Movie
                .Where(x =>
                    (string.IsNullOrEmpty(keyword)
                     || x.OriginalTitle.ToLower().Contains(keyword)
                     || x.Title.ToLower().Contains(keyword))
                    && (!payload.GenreId.HasValue
                        || x.Genres
                            .Select(g => g.IdNumber)
                            .ToList()
                            .Contains(payload.GenreId.Value))
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