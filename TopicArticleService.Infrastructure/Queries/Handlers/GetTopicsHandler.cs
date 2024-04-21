using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetTopicsHandler : IQueryHandler<GetTopicsQuery, List<TopicDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetTopicsHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<TopicDto>> HandleAsync(GetTopicsQuery query)
        {
            var topicReadModels = await _readDbContext
                .Topics
                .Include(x => x.Children)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<TopicDto>>(topicReadModels);
        }
    }
}
