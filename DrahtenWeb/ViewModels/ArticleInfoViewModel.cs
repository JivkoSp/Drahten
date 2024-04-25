using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class ArticleInfoViewModel
    {
        public List<ReadArticleCommentDto> Comments { get; set; }
        public List<ReadUserArticleDto> Views { get; set; }
        public List<ArticleLikeDto> Likes { get; set; }
        public List<ArticleDislikeDto> DisLikes { get; set; }
    }
}
