using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetArticlesHandler : IQueryHandler<GetArticlesQuery, List<ArticleDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetArticlesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<ArticleDto>> HandleAsync(GetArticlesQuery query)
        {
            var articleReadModels = await _readDbContext.Articles
                .Include(x => x.ArticleLikes)
                .Include(x => x.ArticleDislikes)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ArticleDto>>(articleReadModels);
        }
    }
}
