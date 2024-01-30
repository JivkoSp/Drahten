namespace DrahtenWeb.Exceptions
{
    public class HttpContextAccessTokenNotFoundException : ApplicationException
    {
        public HttpContextAccessTokenNotFoundException() : base("No access_token was found in HttpContext.")
        {
                
        }
    }
}
