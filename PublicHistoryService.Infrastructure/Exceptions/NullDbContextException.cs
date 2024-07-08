
namespace PublicHistoryService.Infrastructure.Exceptions
{
    internal class NullDbContextException : InfrastructureException
    {
        internal NullDbContextException() : base(message: "The database context cannot be null!")
        {
        }
    }
}
