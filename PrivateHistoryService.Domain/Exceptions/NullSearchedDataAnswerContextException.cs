
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullSearchedDataAnswerContextException : DomainException
    {
        internal NullSearchedDataAnswerContextException() : base(message: $"The searched data answer context cannot be null!")
        {
        }
    }
}
