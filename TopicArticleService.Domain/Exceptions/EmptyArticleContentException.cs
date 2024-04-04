
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyArticleContentException : DomainException
    {
        internal EmptyArticleContentException() : base(message: "Article content cannot be empty!")
        {
        }
    }
}
