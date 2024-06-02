using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.ReadServices
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
