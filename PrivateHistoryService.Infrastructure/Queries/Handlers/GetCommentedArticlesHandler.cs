using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
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
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<CommentedArticleDto>>(commentedArticleReadModels);
        }
    }
}
