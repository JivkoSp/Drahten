
namespace TopicArticleService.Application.Dtos
{
    public class ArticleDto
    {
        public string ArticleId { get; set; }
        public string PrevTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishingDate { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
        public Guid TopicId { get; set; }
        public string TopicFullName { get; set; }
        public  List<ArticleLikeDto> ArticleLikeDtos { get; set; }
        public  List<ArticleDislikeDto> ArticleDislikeDtos { get; set; }
    }
}
