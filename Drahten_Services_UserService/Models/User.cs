namespace Drahten_Services_UserService.Models
{
    public class User
    {
        //Primary key
        public string UserId { get; set; } = string.Empty;  

        //Relationships
        public virtual HashSet<UserTopic>? Topics { get; set; }
        public virtual HashSet<UserArticle>? UserArticles { get; set; }
        public virtual HashSet<ArticleComment>? ArticleComments { get; set; }
        public virtual HashSet<ArticleLike>? ArticleLikes { get; set; }
        public virtual HashSet<ContactRequest>? ContactRequests { get; set; }
    }
}
