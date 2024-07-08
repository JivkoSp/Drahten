using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.EntityFramework.Services.ReadServices
{
    internal sealed class PostgresSearchedArticleDataReadService : ISearchedArticleDataReadService
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public PostgresSearchedArticleDataReadService(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<SearchedArticleDataDto> GetSearchedArticleDataByIdAsync(Guid searchedArticleDataId)
        {
            var searchedArticleDataReadModel = await _readDbContext.SearchedArticles
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.SearchedArticleDataId == searchedArticleDataId);

            return _mapper.Map<SearchedArticleDataDto>(searchedArticleDataReadModel);
        }
    }
}
