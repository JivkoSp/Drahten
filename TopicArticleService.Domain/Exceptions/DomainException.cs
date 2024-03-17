
namespace TopicArticleService.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public virtual string ErrorCode { get; set; }

        public DomainException(string message) : base(message) 
        {
        }
    }
}
