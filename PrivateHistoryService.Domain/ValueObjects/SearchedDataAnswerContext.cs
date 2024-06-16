using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record SearchedDataAnswerContext
    {
        internal string Value { get; }

        public SearchedDataAnswerContext(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptySearchedDataAnswerContextException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(SearchedDataAnswerContext searchedDataAnswerContext)
            => searchedDataAnswerContext.Value;

        //Conversion from String to ValueObject.
        public static implicit operator SearchedDataAnswerContext(string searchedDataAnswerContext)
            => new SearchedDataAnswerContext(searchedDataAnswerContext);
    }
}
