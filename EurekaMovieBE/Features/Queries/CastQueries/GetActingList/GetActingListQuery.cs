namespace EurekaMovieBE.Features.Queries.CastQueries.GetActingList;

public class GetActingListQuery : IRequest<GetActingListResponse>
{
    public GetActingListRequest Payload { get; set; }
    public GetActingListQuery(GetActingListRequest payload)
    {
        Payload = payload;
    }
}