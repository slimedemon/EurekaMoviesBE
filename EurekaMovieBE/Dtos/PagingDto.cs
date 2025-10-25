namespace EurekaMovieBE.Dtos
{
    public class PagingDto
    {
        public int TotalPage { get; set; }
        public int TotalItem { get; set; }
        public int PageNumber { get; set; }
        public int MaxPerPage { get; set; }
    }
}
