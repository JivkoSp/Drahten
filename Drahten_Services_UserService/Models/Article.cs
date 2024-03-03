namespace Drahten_Services_UserService.Models
{
    public class Article
    {
        //Primary key
        public string ArticleId { get; set; } = string.Empty;
        public string PrevTitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;   

        //Relationships
        public int TopicId { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual ICollection<UserArticle>? UserArticles { get; set; }
        public virtual ICollection<ArticleComment>? ArticleComments { get; set; }
        public virtual ICollection<ArticleLike>? ArticleLikes { get; set; }
    }
}
