namespace EurekaMovieBE.Extensions
{
    public static class LinqExtensions
    {
        public static async Task<PagingDataDto<T>> ToListAsPageAsync<T> (
            this IQueryable<T> query,
            int pageNumber,
            int maxPerPage,
            CancellationToken token) where T : class
        { 
            var result = new PagingDataDto<T>();
            var pagingResponse = new PagingDto
            {
                PageNumber = pageNumber,
                MaxPerPage = maxPerPage,
            };

            var totalItem = await query.CountAsync(token);
            pagingResponse.TotalItem = totalItem;

            var totalPage = (int)Math.Ceiling((double)totalItem / maxPerPage);
            pagingResponse.TotalPage = totalPage;

            var data = await query
                .Skip((pageNumber - 1) * maxPerPage)
                .Take(maxPerPage)
                .AsNoTracking()
                .ToListAsync(token);

            result.Paging = pagingResponse;
            result.Data = data;

            return result;
        }
    }
}
