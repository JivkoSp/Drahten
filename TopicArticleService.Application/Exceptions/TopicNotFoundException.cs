
namespace TopicArticleService.Application.Exceptions
{
    internal class TopicNotFoundException : ApplicationException
    {
        internal TopicNotFoundException(Guid topicId) 
            : base(message: $"Topic #{topicId} was NOT found!")
        {
        }
    }
}
