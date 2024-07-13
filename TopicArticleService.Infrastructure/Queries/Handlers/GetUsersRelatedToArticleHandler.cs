using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetUsersRelatedToArticleHandler : IQueryHandler<GetUsersRelatedToArticleQuery, List<UserDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetUsersRelatedToArticleHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> HandleAsync(GetUsersRelatedToArticleQuery query)
        {
            var userArticleReadModels = await _readDbContext.UserArticles
                                        .Where(x => x.ArticleId == query.ArticleId)
                                        .Include(x => x.User)
                                        .Include(x => x.Article)
                                        .AsNoTracking()
                                        .ToListAsync();

            return _mapper.Map<List<UserDto>>(userArticleReadModels);
        }
    }
}
