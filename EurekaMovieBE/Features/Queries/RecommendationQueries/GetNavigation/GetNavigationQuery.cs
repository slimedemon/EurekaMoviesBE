namespace EurekaMovieBE.Features.Queries.RecommendationQueries.GetNavigation;

public class GetNavigationQuery : IRequest<GetNavigationResponse>
{
    public string Query { get; set; }
    public GetNavigationQuery(string query)
    {
        Query = query;
    }
}