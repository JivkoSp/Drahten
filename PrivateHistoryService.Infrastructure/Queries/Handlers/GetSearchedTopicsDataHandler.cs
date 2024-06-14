using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
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
