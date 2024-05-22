using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GetViewedArticlesHandler : IQueryHandler<GetViewedArticlesQuery, List<ViewedArticleDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetViewedArticlesHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<ViewedArticleDto>> HandleAsync(GetViewedArticlesQuery query)
        {
            var viewedArticleReadModels = await _readDbContext.ViewedArticles
              .Where(x => x.UserId == query.UserId.ToString())
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<ViewedArticleDto>>(viewedArticleReadModels);
        }
    }
}
