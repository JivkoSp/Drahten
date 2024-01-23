namespace DrahtenWeb.Exceptions
{
    public class HttpDeserializedContentNotFoundException : ApplicationException
    {
        public HttpDeserializedContentNotFoundException() : base("Deserialized content from http response was NOT found.")
        {
                
        }
    }
}
