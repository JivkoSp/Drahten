

namespace PrivateHistoryService.Application.Exceptions
{
    public sealed class SearchedTopicNotFoundException : ApplicationException
    {
        internal SearchedTopicNotFoundException(Guid searchedTopicDataId) 
            : base(message: $"Searched topic #{searchedTopicDataId} was NOT found!")
        {
        }
    }
}
