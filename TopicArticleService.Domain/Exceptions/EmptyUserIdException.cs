
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyUserIdException : DomainException
    {
        internal EmptyUserIdException() : base(message: "User id cannot be empty!")
        {
        }
    }
}
