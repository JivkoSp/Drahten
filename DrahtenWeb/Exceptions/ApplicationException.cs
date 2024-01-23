namespace DrahtenWeb.Exceptions
{
    public class ApplicationException : Exception
    {
        public virtual string? Code { get; set; }

        public ApplicationException(string message):base(message) 
        {
                
        }
    }
}
