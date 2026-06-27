using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation(message: "Request started. {context.Request.Path}", context.Request.Path);
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            // get status code
            var statusCode = ex switch
            {
                DomainNotFoundException => (int)HttpStatusCode.NotFound,
                DomainBadRequestException => (int)HttpStatusCode.BadRequest,
                DomainUnauthorizedException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";


            var response = new ErrorResult(statusCode, ex.Message, context.TraceIdentifier);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        public record ErrorResult(int StatusCode, string Message, string TraceId);

    }
}
