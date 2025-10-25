namespace EurekaMovieBE.Dtos.Responses;

public class GetActingListResponse : BaseResponse
{
    public List<People> Data { get; set; } = new();
    public PagingDto Paging { get; set; }
}