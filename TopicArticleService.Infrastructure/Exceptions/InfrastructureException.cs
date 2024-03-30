
namespace TopicArticleService.Infrastructure.Exceptions
{
    internal abstract class InfrastructureException : Exception
    {
        public virtual string ErrorCode { get; set; }

        public InfrastructureException(string message) : base(message)
        {
        }
    }
}
