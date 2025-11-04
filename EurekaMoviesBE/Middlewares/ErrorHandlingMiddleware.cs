using System.Text.Json;

namespace EurekaMoviesBE.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Validation.ValidationException ex)
            {
                context.Response.StatusCode = (int)ResponseStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    status = (int)ResponseStatusCode.BadRequest,
                    errorMessage = "Invalid data value",
                    errors = ex.Errors
                };
                await context.Response.Body.WriteAsync(JsonSerializer.SerializeToUtf8Bytes(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)ResponseStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    status = (int)ResponseStatusCode.InternalServerError,
                    errorMessage = "Internal server error",
                };
                await context.Response.Body.WriteAsync(JsonSerializer.SerializeToUtf8Bytes(response));
            }

        }
    }
}