
namespace PrivateHistoryService.Application.Exceptions
{
    public sealed class SearchedArticleNotFoundException : ApplicationException
    {
        internal SearchedArticleNotFoundException(Guid searchedArticleDataId) 
            : base(message: $"Searched article #{searchedArticleDataId} was NOT found!")
        {
        }
    }
}
