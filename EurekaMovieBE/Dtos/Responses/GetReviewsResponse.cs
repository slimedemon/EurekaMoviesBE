namespace EurekaMovieBE.Dtos.Responses;

public class GetReviewsResponse : BaseResponse
{
    public List<GetReviewsData> Data { get; set; }
    public PagingDto Paging { get; set; }
}

public class GetReviewsData
{
    public long Id { get; set; }
    public string Comment { get; set; }
    public double Stars { get; set; }
    public string UserName { get; set; }
    public string UserAvatar { get; set; }
    public DateTime CreatedDate { get; set; }
}