namespace DrahtenWeb.Exceptions
{
    public class UrlNotFoundException : ApplicationException
    {
        public UrlNotFoundException():base("Http request attempt was made with NO url.")
        {
            
        }
    }
}
