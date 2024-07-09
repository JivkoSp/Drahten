using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Queries;
using PublicHistoryService.Application.Queries.Handlers;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.Queries.Handlers
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
              .Include(x => x.User)
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<ViewedArticleDto>>(viewedArticleReadModels);
        }
    }
}
