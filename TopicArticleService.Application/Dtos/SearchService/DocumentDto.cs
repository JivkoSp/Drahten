
namespace TopicArticleService.Application.Dtos.SearchService
{
    public class DocumentDto
    {
        public string DocumentId { get; set; }
        public string PrevTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishingDate { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
        public string TopicName { get; set; }
    }
}
