
namespace TopicArticleService.Domain.Exceptions
{
    internal abstract class DomainException : Exception
    {
        public virtual string ErrorCode { get; set; }

        internal DomainException(string message) : base(message) 
        {
        }
    }
}
