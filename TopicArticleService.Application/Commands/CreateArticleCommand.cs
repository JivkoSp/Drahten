
namespace TopicArticleService.Application.Commands
{
    public record CreateArticleCommand(Guid ArticleId, string PrevTitle, string Title, string Content, string PublishingDate,
        string Author, string Link, Guid TopicId) : ICommand;
}