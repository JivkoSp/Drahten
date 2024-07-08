
namespace PublicHistoryService.Infrastructure.Exceptions
{
    internal abstract class InfrastructureException : Exception
    {
        public virtual string ErrorCode { get; set; }

        internal InfrastructureException(string message) : base(message)
        {
        }
    }
}
