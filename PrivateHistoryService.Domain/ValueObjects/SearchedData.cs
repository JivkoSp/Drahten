using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record SearchedData
    {
        internal string Value { get; }

        public SearchedData(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptySearchedDataException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(SearchedData searchedData)
            => searchedData.Value;

        //Conversion from String to ValueObject.
        public static implicit operator SearchedData(string searchedData)
            => new SearchedData(searchedData);
    }
}
