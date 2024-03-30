
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticleContentException : DomainException
    {
        internal EmptyArticleContentException() : base(message: "Article content cannot be empty!")
        {
        }
    }
}
