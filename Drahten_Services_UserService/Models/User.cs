namespace Drahten_Services_UserService.Models
{
    public class User
    {
        //Primary key
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;

        //Relationships
        public virtual HashSet<UserTopic>? Topics { get; set; }
        public virtual HashSet<UserArticle>? UserArticles { get; set; }
        public virtual HashSet<ArticleComment>? ArticleComments { get; set; }
        public virtual HashSet<ArticleLike>? ArticleLikes { get; set; }
        public virtual HashSet<ContactRequest>? ContactRequests { get; set; }
        public virtual HashSet<ArticleCommentThumbsUp>? ArticleCommentThumbsUp { get; set; }
        public virtual HashSet<ArticleCommentThumbsDown>? ArticleCommentThumbsDown { get; set; }
    }
}
