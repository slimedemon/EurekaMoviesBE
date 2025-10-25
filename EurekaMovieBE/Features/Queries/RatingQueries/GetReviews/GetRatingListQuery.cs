using MediatR;
using EurekaMovieBE.Dtos.Requests;
using EurekaMovieBE.Dtos.Responses;

namespace EurekaMovieBE.Features.Queries.RatingQueries.GetReviews;

public class GetRatingListQuery : IRequest<GetRatingListResponse>
{
    public GetRatingListRequest Payload { get; set; }
    public GetRatingListQuery(GetRatingListRequest payload)
    {
        Payload = payload;
    }
}