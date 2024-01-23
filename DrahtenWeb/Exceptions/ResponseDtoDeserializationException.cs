namespace DrahtenWeb.Exceptions
{
    public class ResponseDtoDeserializationException : ApplicationException
    {
        public ResponseDtoDeserializationException() : base("ResponseDto object was NOT deserialized properly.")
        {
                
        }
    }
}
