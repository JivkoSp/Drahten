
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptySearchedDataAnswerException : DomainException
    {
        internal EmptySearchedDataAnswerException() : base(message: $"The searched data answer cannot be empty!")
        {
        }
    }
}
