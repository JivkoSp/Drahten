using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetArticleDislikesHandler : IQueryHandler<GetArticleDislikesQuery, List<ArticleDislikeDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetArticleDislikesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<ArticleDislikeDto>> HandleAsync(GetArticleDislikesQuery query)
        {
            var articleDislikeReadModels = await _readDbContext.ArticleDislikes
               .Where(x => x.ArticleId == query.ArticleId)
               .AsNoTracking()
               .SingleOrDefaultAsync();

            return _mapper.Map<List<ArticleDislikeDto>>(articleDislikeReadModels);
        }
    }
}
