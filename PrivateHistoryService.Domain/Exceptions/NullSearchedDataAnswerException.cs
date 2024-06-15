
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullSearchedDataAnswerException : DomainException
    {
        internal NullSearchedDataAnswerException() : base(message: $"The searched data answer cannot be null!")
        {
        }
    }
}
