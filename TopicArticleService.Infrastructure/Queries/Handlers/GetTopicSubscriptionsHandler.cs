using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetTopicSubscriptionsHandler : IQueryHandler<GetTopicSubscriptionsQuery, List<UserTopicDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetTopicSubscriptionsHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<UserTopicDto>> HandleAsync(GetTopicSubscriptionsQuery query)
        {
            var userTopicReadModels = await _readDbContext.UserTopics
                .Where(x => x.TopicId == query.TopicId)
                .Include(x => x.Topic)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<UserTopicDto>>(userTopicReadModels);
        }
    }
}
