namespace EurekaMoviesBE.Dtos.Responses;

public class GetListFavoriteResponse : BaseResponse
{
    public PagingDto Paging { get; set; }
    public List<GetListFavoriteData> Data { get; set; }
}

public class GetListFavoriteData
{
    public long FavoriteId { get; set; }
    public long TmdbId { get; set; }
    public Movie Movie { get; set; }
}