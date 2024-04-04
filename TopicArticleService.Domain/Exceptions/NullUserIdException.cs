
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullUserIdException : DomainException
    {
        internal NullUserIdException() : base(message: "UserId cannot be null!")
        {
        }
    }
}
