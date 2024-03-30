
namespace TopicArticleService.Domain.Exceptions
{
    internal class NullUserIdException : DomainException
    {
        public NullUserIdException() : base(message: "UserId cannot be null!")
        {
        }
    }
}
