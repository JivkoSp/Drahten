using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetArticleCommentsHandler : IQueryHandler<GetArticleCommentsQuery, List<ArticleCommentDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetArticleCommentsHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<ArticleCommentDto>> HandleAsync(GetArticleCommentsQuery query)
        {
            var articleCommentReadModels = await _readDbContext.ArticleComments
                .Where(x => x.ArticleId == query.ArticleId)
                .Include(x => x.Children)
                .Include(x => x.Article)
                .Include(x => x.ArticleCommentLikes)
                .Include(x => x.ArticleCommentDislikes)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ArticleCommentDto>>(articleCommentReadModels);
        }
    }
}
