using Drahten_ApiGateway_Yarp.Options;

namespace Drahten_ApiGateway_Yarp.Middlewares
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RateLimitOptions _options;
        private static readonly Dictionary<string, int> RequestCounts = new();
        private static readonly Dictionary<string, DateTime> Blacklist = new();

        public RateLimitMiddleware(RequestDelegate next, RateLimitOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();

            if (clientIp == null)
            {
                // If we can't get the client IP address, log an error and proceed.
                Console.WriteLine("Yarp --> Client IP address could not be determined!");
                await _next(context);
                return;
            }

            Console.WriteLine($"Yarp --> Request received from IP: {clientIp}");

            if (Blacklist.ContainsKey(clientIp) && Blacklist[clientIp] > DateTime.UtcNow)
            {
                Console.WriteLine($"Yarp --> Blacklisted IP {clientIp} attempted to access the service!");
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
                    Console.WriteLine($"Yarp --> Rate limit exceeded for IP {clientIp}. Request count: {RequestCounts[clientIp]}");

                    // Increment violation count
                    if (!context.Items.ContainsKey("ViolationCount"))
                    {
                        context.Items["ViolationCount"] = 0;
                    }

                    context.Items["ViolationCount"] = (int)context.Items["ViolationCount"] + 1;

                    Console.WriteLine($"Yarp --> Violation count for IP {clientIp}: {context.Items["ViolationCount"]}");

                    // Check if the client should be blacklisted
                    if ((int)context.Items["ViolationCount"] > _options.ViolationThreshold)
                    {
                        Blacklist[clientIp] = DateTime.UtcNow.AddMinutes(_options.BlacklistDurationMinutes);

                        Console.WriteLine($"Yarp --> IP {clientIp} has been blacklisted until {Blacklist[clientIp]}");
                    }

                    // Reset request count for the period
                    RequestCounts[clientIp] = 0;

                    // Return 429 Too Many Requests status code
                    context.Response.StatusCode = _options.ViolationHttpStatusCode;

                    Console.WriteLine($"Yarp --> Response for IP {clientIp} set to {_options.ViolationHttpStatusCode} --> Too Many Requests");

                    return;
                }
            }

            await _next(context);
        }
    }
}
