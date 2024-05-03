using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetUserArticlesHandler : IQueryHandler<GetUserArticlesQuery, List<ArticleDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetUserArticlesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<ArticleDto>> HandleAsync(GetUserArticlesQuery query)
        {
            var articleReadModels = await _readDbContext.UserTopics
                                    .Join(_readDbContext.Topics,
                                          left_side => left_side.TopicId,
                                          right_side => right_side.TopicId,
                                          (left_side, right_side) => new { UserId = left_side.UserId,
                                                                           TopicId = left_side.TopicId,
                                                                           TopicFullName = right_side.TopicFullName })
                                    .Join(_readDbContext.Articles,
                                          left_side => left_side.TopicId,
                                          right_side => right_side.TopicId,
                                          (left_side, right_side) => new { UserId = left_side.UserId,
                                                                           ArticleId = right_side.ArticleId,
                                                                           ArticlePrevTitle = right_side.PrevTitle,
                                                                           ArticleTitle = right_side.Title,
                                                                           ArticleContent = right_side.Content,
                                                                           ArticlePublishingDate = right_side.PublishingDate,
                                                                           ArticleAuthor = right_side.Author,
                                                                           ArticleLink = right_side.Link,
                                                                           ArticleTopicId = right_side.TopicId,
                                                                           ArticleLikes = right_side.ArticleLikes,
                                                                           ArticleDislikes = right_side.ArticleDislikes,
                                                                           TopicFullName = left_side.TopicFullName })
                                    .Where(x => x.UserId == query.UserId)
                                    .Select(x => new ArticleReadModel
                                    {
                                        ArticleId = x.ArticleId,
                                        PrevTitle = x.ArticlePrevTitle,
                                        Title = x.ArticleTitle,
                                        Content = x.ArticleContent,
                                        PublishingDate = x.ArticlePublishingDate,
                                        Author = x.ArticleAuthor,
                                        Link = x.ArticleLink,
                                        TopicId = x.ArticleTopicId,
                                        ArticleLikes = x.ArticleLikes,
                                        ArticleDislikes= x.ArticleDislikes,
                                        Topic = new TopicReadModel { TopicFullName = x.TopicFullName },
                                    })
                                    .AsNoTracking()
                                    .ToListAsync();

            return _mapper.Map<List<ArticleDto>>(articleReadModels);
        }
    }
}
