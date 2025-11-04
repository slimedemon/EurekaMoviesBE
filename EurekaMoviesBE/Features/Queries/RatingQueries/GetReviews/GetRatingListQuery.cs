using MediatR;
using EurekaMoviesBE.Dtos.Requests;
using EurekaMoviesBE.Dtos.Responses;

namespace EurekaMoviesBE.Features.Queries.RatingQueries.GetReviews;

public class GetRatingListQuery : IRequest<GetRatingListResponse>
{
    public GetRatingListRequest Payload { get; set; }
    public GetRatingListQuery(GetRatingListRequest payload)
    {
        Payload = payload;
    }
}