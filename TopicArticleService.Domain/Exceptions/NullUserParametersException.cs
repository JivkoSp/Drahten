
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullUserParametersException : DomainException
    {
        internal NullUserParametersException() : base(message: $"The User cannot be initialized with one or more null parameters!")
        {
        }
    }
}
