
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptySearchedDataException : DomainException
    {
        internal EmptySearchedDataException() : base(message: "Searched data cannot be empty!")
        {
        }
    }
}
