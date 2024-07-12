using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticlePrevTitle
    {
        internal string Value { get; }

        public ArticlePrevTitle(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyArticlePrevTitleException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(ArticlePrevTitle prevTitle) 
            => prevTitle.Value;

        //Conversion from String to ValueObject.
        public static implicit operator ArticlePrevTitle(string prevTitle)
            => new ArticlePrevTitle(prevTitle);
    }
}
