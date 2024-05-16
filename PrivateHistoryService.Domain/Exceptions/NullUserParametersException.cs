
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullUserParametersException : DomainException
    {
        public NullUserParametersException() : base(message: $"The User cannot be initialized with one or more null parameters!")
        {
        }
    }
}
