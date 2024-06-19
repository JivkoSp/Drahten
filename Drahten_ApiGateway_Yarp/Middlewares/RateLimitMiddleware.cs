using Drahten_ApiGateway_Yarp.Options;

namespace Drahten_ApiGateway_Yarp.Middlewares
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RateLimitOptions _options;
        private readonly ILogger<RateLimitMiddleware> _logger;
        private static readonly Dictionary<string, int> RequestCounts = new();
        private static readonly Dictionary<string, int> ViolationCounts = new();
        private static readonly Dictionary<string, DateTime> Blacklist = new();

        public RateLimitMiddleware(RequestDelegate next, RateLimitOptions options, ILogger<RateLimitMiddleware> logger)
        {
            _next = next;
            _options = options;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();

            if (clientIp == null)
            {
                _logger.LogError("Yarp Gateway --> Client IP address could not be determined!");

                await _next(context);
                return;
            }

            _logger.LogInformation($"Yarp Gateway --> Request received from IP: {clientIp}");

            if (Blacklist.ContainsKey(clientIp) && Blacklist[clientIp] > DateTime.UtcNow)
            {
                _logger.LogWarning($"Yarp Gateway --> Blacklisted IP {clientIp} attempted to access the service!");

                context.Response.StatusCode = _options.BlacklistHttpStatusCode;
                return;
            }

            if (clientIp != null)
            {
                if (!RequestCounts.ContainsKey(clientIp))
                {
                    RequestCounts[clientIp] = 0;
                }

                RequestCounts[clientIp]++;

                if (RequestCounts[clientIp] > _options.RequestLimit)
                {
                    _logger.LogWarning($"Yarp Gateway --> Rate limit exceeded for IP {clientIp}. Request count: {RequestCounts[clientIp]}");

                    // Increment violation count
                    if (!ViolationCounts.ContainsKey(clientIp))
                    {
                        ViolationCounts[clientIp] = 0;
                    }

                    ViolationCounts[clientIp] = ViolationCounts[clientIp] + 1;

                    _logger.LogInformation($"Yarp Gateway --> Violation count for IP {clientIp}: {ViolationCounts[clientIp]}");

                    // Check if the client should be blacklisted
                    if (ViolationCounts[clientIp] > _options.ViolationThreshold)
                    {
                        Blacklist[clientIp] = DateTime.UtcNow.AddMinutes(_options.BlacklistDurationMinutes);

                        _logger.LogWarning($"Yarp Gateway --> IP {clientIp} has been blacklisted until {Blacklist[clientIp]}");
                    }

                    // Reset request count for the period
                    RequestCounts[clientIp] = 0;

                    // Return 429 Too Many Requests status code
                    context.Response.StatusCode = _options.ViolationHttpStatusCode;

                    _logger.LogWarning($"Yarp Gateway --> Response for IP {clientIp} set to {_options.ViolationHttpStatusCode} --> Too Many Requests!");

                    return;
                }
            }

            await _next(context);
        }
    }
}
