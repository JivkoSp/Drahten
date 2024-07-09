using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Queries;
using PublicHistoryService.Application.Queries.Handlers;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GetSearchedArticlesDataHandler : IQueryHandler<GetSearchedArticlesDataQuery, List<SearchedArticleDataDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetSearchedArticlesDataHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<SearchedArticleDataDto>> HandleAsync(GetSearchedArticlesDataQuery query)
        {
            var searchedArticleReadModels = await _readDbContext.SearchedArticles
              .Where(x => x.UserId == query.UserId.ToString())
              .Include(x => x.User)
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<SearchedArticleDataDto>>(searchedArticleReadModels);
        }
    }
}
