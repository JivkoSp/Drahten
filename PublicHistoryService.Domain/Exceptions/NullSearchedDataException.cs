
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class NullSearchedDataException : DomainException
    {
        internal NullSearchedDataException() : base(message: "Searched data cannot be null!")
        {
        }
    }
}
