using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetArticleHandler : IQueryHandler<GetArticleQuery, ArticleDto>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetArticleHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<ArticleDto> HandleAsync(GetArticleQuery query)
        {
            var articleReadModel = await _readDbContext.Articles
                .Where(x => x.ArticleId == query.ArticleId)
                .Include(x => x.ArticleLikes)
                .Include(x => x.ArticleDislikes)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return _mapper.Map<ArticleDto>(articleReadModel);
        }
    }
}
