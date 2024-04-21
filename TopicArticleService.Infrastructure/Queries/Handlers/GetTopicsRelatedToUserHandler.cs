using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetTopicsRelatedToUserHandler : IQueryHandler<GetTopicsRelatedToUserQuery, List<UserTopicDto>>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetTopicsRelatedToUserHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<List<UserTopicDto>> HandleAsync(GetTopicsRelatedToUserQuery query)
        {
            //This query performs two inner joins between three tables, who are in many-to-many relationship.
            //The UserTopic table is join table between the User and Topic tables.
            //The User table is joined with the UserTopic table on the condition that UserId from the User table
            //is equal to the UserId from the UserTopic table.
            //The Topic table is then joined on the condition that the TopicId from the UserTopic table
            //is equal to the TopicId from the Topic table.
            var userTopicDtos = await _readDbContext.Users
                .Join(_readDbContext.UserTopics, left_side => left_side.UserId, right_side => right_side.UserId,
                    (left_side, right_side) => new {
                        UserId = left_side.UserId,
                        TopicId = right_side.TopicId,
                        SubscriptionTime = right_side.SubscriptionTime
                }).Where(x => x.UserId == query.UserId.ToString())
                .Join(_readDbContext.Topics, left_side => left_side.TopicId, right_side => right_side.TopicId,
                    (left_side, right_side) => new {
                        UserId = left_side.UserId,
                        TopicId = left_side.TopicId,
                        SubscriptionTime = left_side.SubscriptionTime,
                        TopicName = right_side.TopicName
                }).Select(x => new UserTopicDto
                    {
                         UserId = x.UserId,
                         TopicId = x.TopicId,
                         TopicName = x.TopicName,
                         SubscriptionTime = x.SubscriptionTime
                    })
                .ToListAsync();

            return userTopicDtos;
        }
    }
}
