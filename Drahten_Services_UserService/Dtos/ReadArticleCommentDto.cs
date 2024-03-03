using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Dtos
{
    public class ReadArticleCommentDto
    {
        public int ArticleCommentId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public string ArticleId { get; set; } = string.Empty;
        public ReadUserDto? UserDto { get; set; }
        public int? ParentArticleCommentId { get; set; }
        public virtual ICollection<ReadArticleCommentDto>? Children { get; set; }
        public virtual HashSet<ReadArticleCommentThumbsUpDto>? ArticleCommentThumbsUp { get; set; }
        public virtual HashSet<ReadArticleCommentThumbsDownDto>? ArticleCommentThumbsDown { get; set; }
    }
}
