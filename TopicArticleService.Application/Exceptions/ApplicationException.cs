
namespace TopicArticleService.Application.Exceptions
{
    internal abstract class ApplicationException : Exception
    {
        public virtual string ErrorCode { get; set; }

        internal ApplicationException(string message) : base(message) 
        {
        }
    }
}
