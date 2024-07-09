using System.Threading.RateLimiting;

namespace PublicHistoryService.Presentation.Middlewares
{
    public class RateLimitingMiddleware : IMiddleware
    {
        private readonly TokenBucketRateLimiter _rateLimiter;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        public RateLimitingMiddleware(TokenBucketRateLimiter rateLimiter, ILogger<RateLimitingMiddleware> logger)
        {
            _rateLimiter = rateLimiter;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var clientIP = context.Connection.RemoteIpAddress?.ToString();

            try
            {
                var lease = _rateLimiter.AttemptAcquire();

                if (!lease.IsAcquired)
                {
                    // Log denied request due to rate limit exceeded
                    _logger.LogWarning($"PrivateHistoryService --> Request from IP Address: {clientIP} rejected due to rate limit exceeded!");

                    context.Items["RateLimitExceeded"] = true;
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                    return;
                }

                // Log successful token acquisition
                _logger.LogInformation($"PrivateHistoryService --> Token used by IP {clientIP}");

                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"PrivateHistoryService --> Error processing request from IP: {clientIP}");

                throw; // Rethrow the exception for the global error handler middleware
            }
        }
    }
}
