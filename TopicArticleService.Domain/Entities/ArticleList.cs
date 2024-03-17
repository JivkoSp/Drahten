using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Entities
{
    public class ArticleList : AggregateRoot<ArticleListId>
    {
        private List<Article> _articles = new List<Article>();

        public ArticleList(ArticleListId id)
        {
            Id = id;
        }

        public void AddArticle(Article article)
        {

        }

        public void RemoveArticle(ArticleId articleId)
        {

        }
    }
}
