using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class SemanticSearchViewModel
    {
        public List<ArticleDto> Articles { get; set; } = new List<ArticleDto>();
        public Dictionary<string, List<ReadArticleCommentDto>> ArticleComments { get; set; } = new Dictionary<string, List<ReadArticleCommentDto>>();
        public Dictionary<string, List<ReadUserArticleDto>> UsersRelatedToArticle { get; set; } = new Dictionary<string, List<ReadUserArticleDto>>();
    }
}
