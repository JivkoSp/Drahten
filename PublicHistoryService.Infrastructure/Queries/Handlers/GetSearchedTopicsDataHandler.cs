using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Application.Queries;
using PublicHistoryService.Application.Queries.Handlers;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GetSearchedTopicsDataHandler : IQueryHandler<GetSearchedTopicsDataQuery, List<SearchedTopicDataDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetSearchedTopicsDataHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<SearchedTopicDataDto>> HandleAsync(GetSearchedTopicsDataQuery query)
        {
            var searchedTopicReadModels = await _readDbContext.SearchedTopics
              .Where(x => x.UserId == query.UserId.ToString())
              .Include(x => x.User)
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<SearchedTopicDataDto>>(searchedTopicReadModels);
        }
    }
}
