using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class ArticleViewModel
    {
        public ArticleDto Article { get; set; } = new ArticleDto();
        public List<ReadUserArticleDto> UserArticles { get; set; } = new List<ReadUserArticleDto>();
        public List<ReadArticleCommentDto> ArticleComments { get; set; } = new List<ReadArticleCommentDto>();
        public List<Dtos.UserService.UserDto> Users { get; set; } = new List<Dtos.UserService.UserDto>();
    }
}
