namespace EurekaMoviesBE.Features.Queries.RatingQueries.GetRatingList;

public class GetReviewsQuery : IRequest<GetReviewsResponse>
{
    public GetReviewsRequest Payload { get; set; }
    public GetReviewsQuery(GetReviewsRequest payload)
    {
        Payload = payload;
    }
}