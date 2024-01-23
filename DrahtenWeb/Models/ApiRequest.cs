namespace DrahtenWeb.Models
{
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class ApiRequest
    {
        public ApiType? ApiType { get; set; }
        public string? Url { get; set; }
        public string? Data { get; set; }
        public string? AccessToken { get; set; }
    }
}
