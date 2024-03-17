
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticleCommentIdException : DomainException
    {
        public EmptyArticleCommentIdException() : base(message: "ArticleComment id cannot be empty!")
        {
        }
    }
}
