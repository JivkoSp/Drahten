
namespace UserService.Domain.Exceptions
{
    public sealed class NullUserParameterException : DomainException
    {
        internal NullUserParameterException() 
            : base(message: $"The User cannot be initialized with one or more null parameters!")
        {
        }
    }
}
