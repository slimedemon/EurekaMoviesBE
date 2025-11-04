namespace EurekaMoviesBE.Features.Queries.GenreQueries.GetGenres;

public class GetGenresHandler : IRequestHandler<GetGenresQuery, GetGenresResponse>
{
    private readonly ITmdbUnitOfWork _unitOfRepository;
    private readonly ILogger<GetGenresHandler> _logger;

    public GetGenresHandler(ITmdbUnitOfWork unitOfRepository, ILogger<GetGenresHandler> logger)
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    public async Task<GetGenresResponse> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        const string functionName = $"{nameof(GetGenresHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetGenresResponse{ Status = (int)ResponseStatusCode.Ok };

        try
        {
            var genres = await _unitOfRepository.MovieGenre
                .GetAll()
                .ToListAsync(cancellationToken);
           
            response.Data = genres;
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