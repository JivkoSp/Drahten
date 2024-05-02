using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Services
{
    internal sealed class PostgreTopicReadServices : ITopicReadService
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public PostgreTopicReadServices(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public Task<bool> ExistsByIdAsync(Guid id)
            => _readDbContext.Topics.AnyAsync(x => x.TopicId == id);

        public async Task<TopicDto> GetTopicByNameAsync(string topicName)
        {
            var topicReadModel = await _readDbContext.Topics
            .FirstOrDefaultAsync(x => x.TopicFullName == topicName);

            return _mapper.Map<TopicDto>(topicReadModel);
        }
    }
}
