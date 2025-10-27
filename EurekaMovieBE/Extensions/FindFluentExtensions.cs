namespace EurekaMovieBE.Extensions
{
    public static class FindFluentExtensions
    {
        public static async Task<PagingDataDto<T>> ToListAsPageAsync<T>(this IFindFluent<T, T> query, int pageNumber, int maxPerPage, CancellationToken cancelToken) where T : class
        {
            var result = new PagingDataDto<T>();
            var pagingResponse = new PagingDto
            {
                MaxPerPage = maxPerPage,
                PageNumber = pageNumber
            };
            var totalItem = await query.CountDocumentsAsync(cancelToken);
            if (totalItem == 0)
            {
                result.Paging = pagingResponse;
                return result;
            }

            pagingResponse.TotalItem = (int)totalItem;
            pagingResponse.TotalPage = (int)Math.Ceiling((float)totalItem / maxPerPage);
            var data = await query
                .Skip((pageNumber - 1) * maxPerPage)
                .Limit(maxPerPage)
                .ToListAsync(cancelToken);

            result.Paging = pagingResponse;
            result.Data = data;
            return result;
        }
    }
}
