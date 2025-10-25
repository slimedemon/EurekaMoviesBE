namespace EurekaMovieBE.Features.Queries.MovieQueries.GetLatestTrailers;

public class GetLatestTrailersHandler : IRequestHandler<GetLatestTrailersQuery, GetLatestTrailersResponse>
{
    private readonly ITmdbUnitOfWork _unitOfRepository;
    private readonly ILogger<GetLatestTrailersHandler> _logger;

    public GetLatestTrailersHandler
    (
        ITmdbUnitOfWork unitOfRepository, 
        ILogger<GetLatestTrailersHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
    }
    
    public async Task<GetLatestTrailersResponse> Handle(GetLatestTrailersQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        const string functionName = $"{nameof(GetLatestTrailersHandler)} =>";
        _logger.LogInformation(functionName);
        
        var response = new GetLatestTrailersResponse{ Status = (int)ResponseStatusCode.Ok };

        try
        {
            var currentDate = DateTime.UtcNow;

            var movies = await _unitOfRepository.Movie
                .Where(x =>
                    x.Trailers != null && x.Trailers.Count > 0 &&
                    x.Status != "Released" &&
                    x.ReleaseDate >= currentDate)
                .OrderByDescending(x => x.ReleaseDate)
                .ToListAsync(cancellationToken);

            var latestTrailersData = movies
                .SelectMany(movie => movie.Trailers, (movie, trailer) => new GetLatestTrailersData
                {
                    Trailer = trailer,
                    Movie = movie
                })
                .OrderByDescending(x => x.Trailer.PublishedAt)
                .ToList();
            var paging = new PagingDto
            {
                PageNumber = payload.PageNumber,
                MaxPerPage = payload.MaxPerPage,
                TotalItem = latestTrailersData.Count,
                TotalPage = (int)Math.Ceiling(latestTrailersData.Count / (double)payload.MaxPerPage)
            };
            response.Paging = paging;
            if (paging.TotalPage == 0)
            {
                return response;
            }
            
            response.Data = latestTrailersData
                .Skip((paging.PageNumber - 1) * paging.MaxPerPage)
                .Take(paging.MaxPerPage)
                .ToList();
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