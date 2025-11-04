namespace EurekaMoviesBE.Features.Queries.MovieQueries.GetLatestTrailers;

public class GetLatestTrailersQuery : IRequest<GetLatestTrailersResponse>
{
    public GetLatestTrailersRequest Payload { get; set; }
    public GetLatestTrailersQuery(GetLatestTrailersRequest payload)
    {
        Payload = payload;
    }
}