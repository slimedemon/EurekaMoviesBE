namespace EurekaMovieBE.Features.Queries.WatchListQueries.GetWatchList;

public class GetWatchListQuery : IRequest<GetWatchListResponse>
{
    public GetWatchListRequest Payload { get; set; }
    public GetWatchListQuery(GetWatchListRequest payload)
    {
        Payload = payload;
    }
}