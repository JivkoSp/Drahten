using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public sealed class ArticleFactory : IArticleFactory
    {
        public Article Create(ArticleID id, ArticlePrevTitle prevTitle, ArticleTitle title, ArticleContent content, 
            ArticlePublishingDate publishingDate, ArticleAuthor author, ArticleLink link, TopicId topicId)
            => new Article(id, prevTitle, title, content, publishingDate, author, link, topicId);
    }
}
