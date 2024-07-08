using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.EntityFramework.Services.ReadServices
{
    internal sealed class PostgresSearchedTopicDataReadService : ISearchedTopicDataReadService
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public PostgresSearchedTopicDataReadService(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<SearchedTopicDataDto> GetSearchedTopicDataByIdAsync(Guid searchedTopicDataId)
        {
            var searchedTopicDataReadModel = await _readDbContext.SearchedTopics
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.SearchedTopicDataId == searchedTopicDataId);

            return _mapper.Map<SearchedTopicDataDto>(searchedTopicDataReadModel);
        }
    }
}
