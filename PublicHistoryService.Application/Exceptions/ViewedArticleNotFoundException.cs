
namespace PublicHistoryService.Application.Exceptions
{
    public sealed class ViewedArticleNotFoundException : ApplicationException
    {
        internal ViewedArticleNotFoundException(Guid viewedArticleId)
            : base(message: $"Viewed article ${viewedArticleId} was NOT found!")
        {
        }
    }
}
