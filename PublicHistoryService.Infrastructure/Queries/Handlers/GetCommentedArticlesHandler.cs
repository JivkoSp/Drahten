using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Queries;
using PublicHistoryService.Application.Queries.Handlers;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GetCommentedArticlesHandler : IQueryHandler<GetCommentedArticlesQuery, List<CommentedArticleDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetCommentedArticlesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<CommentedArticleDto>> HandleAsync(GetCommentedArticlesQuery query)
        {
            var commentedArticleReadModels = await _readDbContext.CommentedArticles
              .Where(x => x.UserId == query.UserId.ToString())
              .Include(x => x.User)
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<CommentedArticleDto>>(commentedArticleReadModels);
        }
    }
}
