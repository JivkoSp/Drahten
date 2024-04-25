using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetArticleLikesHandler : IQueryHandler<GetArticleLikesQuery, List<ArticleLikeDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetArticleLikesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<ArticleLikeDto>> HandleAsync(GetArticleLikesQuery query)
        {
            var articleLikeReadModels = await _readDbContext.ArticleLikes
                .Where(x => x.ArticleId == query.ArticleId)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ArticleLikeDto>>(articleLikeReadModels);
        }
    }
}
