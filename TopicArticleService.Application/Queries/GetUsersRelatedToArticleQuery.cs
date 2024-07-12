using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetUsersRelatedToArticleQuery(string ArticleId) : IQuery<List<UserDto>>; 
}
