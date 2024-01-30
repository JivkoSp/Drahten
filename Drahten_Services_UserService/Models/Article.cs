namespace Drahten_Services_UserService.Models
{
    public class Article
    {
        //Primary key
        public string ArticleId { get; set; } = string.Empty;
        public string ArticleData { get; set; } = string.Empty;

        //Relationships
        public int TopicId { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual ICollection<UserArticle>? UserArticles { get; set; }
        public virtual ICollection<ArticleComment>? ArticleComments { get; set; }
        public virtual ICollection<ArticleLike>? ArticleLikes { get; set; }
    }
}
