
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullSearchedDataException : DomainException
    {
        public NullSearchedDataException() : base(message: "Searched data cannot be null!")
        {
        }
    }
}
