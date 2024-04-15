
namespace UserService.Application.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        public virtual string ErrorCode { get; set; }

        public ApplicationException(string message) : base(message)
        {
        }
    }
}
