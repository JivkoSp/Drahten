using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record SearchedDataAnswer
    {
        internal string Value { get; }

        public SearchedDataAnswer(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptySearchedDataAnswerException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(SearchedDataAnswer searchedDataAnswer)
            => searchedDataAnswer.Value;

        //Conversion from String to ValueObject.
        public static implicit operator SearchedDataAnswer(string searchedDataAnswer)
            => new SearchedDataAnswer(searchedDataAnswer);
    }
}
