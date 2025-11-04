namespace EurekaMoviesBE.Dtos
{
    public class PagingDataDto<T>
    {
        public List<T> Data { get; set; } = [];
        public PagingDto Paging { get; set; } = default!;
    }
}
