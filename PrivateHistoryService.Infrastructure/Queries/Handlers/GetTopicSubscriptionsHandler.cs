using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Handlers;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.Queries.Handlers
{
    internal sealed class GetTopicSubscriptionsHandler : IQueryHandler<GetTopicSubscriptionsQuery, List<TopicSubscriptionDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetTopicSubscriptionsHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<TopicSubscriptionDto>> HandleAsync(GetTopicSubscriptionsQuery query)
        {
            var topicSubscriptionReadModels = await _readDbContext.TopicSubscriptions
              .Where(x => x.UserId == query.UserId.ToString())
              .Include(x => x.User)
              .AsNoTracking()
              .ToListAsync();

            return _mapper.Map<List<TopicSubscriptionDto>>(topicSubscriptionReadModels);
        }
    }
}
