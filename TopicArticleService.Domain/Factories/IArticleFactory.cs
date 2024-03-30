using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public interface IArticleFactory
    {
        Article Create(ArticleID id, ArticlePrevTitle prevTitle, ArticleTitle title, ArticleContent content,
            ArticlePublishingDate publishingDate, ArticleAuthor author, ArticleLink link, TopicId topicId);
    }
}
