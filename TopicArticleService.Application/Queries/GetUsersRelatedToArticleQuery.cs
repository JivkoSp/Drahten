using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetUsersRelatedToArticleQuery : IQuery<List<UserArticleDto>> //TODO: Change the dto to UserDto (there is no reason to returns the article dto)
    {
        public string ArticleId { get; set; }
    }
}
