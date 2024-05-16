
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptySearchedDataException : DomainException
    {
        public EmptySearchedDataException() : base(message: "Searched data cannot be empty!")
        {
        }
    }
}
