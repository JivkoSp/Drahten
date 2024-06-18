namespace Drahten_ApiGateway_Yarp.Options
{
    public class RateLimitOptions
    {
        public int RequestLimit { get; set; } = 50;
        public int ViolationThreshold { get; set; } = 5;
        public int BlacklistDurationMinutes { get; set; } = 60;
        public int BlacklistHttpStatusCode { get; set; } = StatusCodes.Status403Forbidden;
        public int ViolationHttpStatusCode { get; set; } = StatusCodes.Status429TooManyRequests;
    }
}
