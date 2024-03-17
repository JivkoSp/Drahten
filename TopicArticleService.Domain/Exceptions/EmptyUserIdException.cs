
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyUserIdException : DomainException
    {
        public EmptyUserIdException() : base(message: "User id cannot be empty!")
        {
        }
    }
}
