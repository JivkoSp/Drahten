
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptySearchedDataAnswerContextException : DomainException
    {
        internal EmptySearchedDataAnswerContextException() : base(message: $"The searched data answer context cannot be empty!")
        {
        }
    }
}
