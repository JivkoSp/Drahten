
namespace TopicArticleService.Infrastructure.Exceptions
{
    internal class RabbitMqInitializationException : InfrastructureException
    {
        internal RabbitMqInitializationException() : base(message: $"Failed to initialize RabbitMQ connection!")
        {
        }
    }
}
