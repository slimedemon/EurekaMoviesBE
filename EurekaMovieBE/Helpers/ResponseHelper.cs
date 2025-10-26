using Microsoft.AspNetCore.Mvc;

namespace EurekaMovieBE.Helpers
{
    public class ResponseHelper
    {
        public static ObjectResult ToResponse(int status, string errorMessage = "", object? data = null)
        {
            var response = new
            {
                Status = status,
                ErrorMessage = errorMessage,
                Data = data
            };
            return new ObjectResult(response) { StatusCode = status };
        }

        public static ObjectResult ToPaginationResponse(int status, string errorMessage = "", object? data = null, PagingDto? paging = null)
        {
            var response = new
            {
                Status = status,
                ErrorMessage = errorMessage,
                Data = data,
                Paging = paging
            };
            return new ObjectResult(response) { StatusCode = status };
        }
    }
}
