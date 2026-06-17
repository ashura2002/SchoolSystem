using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            // get status code
            var statusCode = ex switch
            {
                UserNotFoundException => (int)HttpStatusCode.NotFound,
                UserInvalidValueException => (int)HttpStatusCode.BadGateway,
                UserUnauthorizedException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";


            var response = new ErrorResult(statusCode, ex.Message);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        public record ErrorResult(int StatusCode, string Message);

    }
}
