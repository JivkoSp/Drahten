using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
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
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<SearchedArticleDataDto>>(searchedArticleReadModels);
        }
    }
}
