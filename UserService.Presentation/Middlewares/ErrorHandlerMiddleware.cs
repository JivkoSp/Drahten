using System.Net;
using System.Text.Json;
using UserService.Infrastructure.Exceptions.Interfaces;

namespace UserService.Presentation.Middlewares
{
    public sealed class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionToResponseMapper _exceptionToResponseMapper;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(IExceptionToResponseMapper exceptionToResponseMapper, ILogger<ErrorHandlerMiddleware> logger)
        {
            _exceptionToResponseMapper = exceptionToResponseMapper;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleErrorAsync(context, ex);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception ex)
        {
            var exceptionResponse = _exceptionToResponseMapper.Map(ex);

            context.Response.StatusCode = (int)(exceptionResponse?.StatusCode ?? HttpStatusCode.BadRequest);

            if (exceptionResponse?.Response == null)
            {
                await context.Response.WriteAsync(string.Empty);

                return;
            }

            context.Response.ContentType = "application/json";

            await JsonSerializer.SerializeAsync(context.Response.Body, exceptionResponse.Response);
        }
    }
}
