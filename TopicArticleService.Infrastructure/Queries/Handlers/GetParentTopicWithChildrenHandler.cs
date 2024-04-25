using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Handlers;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.Queries.Handlers
{
    internal sealed class GetParentTopicWithChildrenHandler : IQueryHandler<GetParentTopicWithChildrenQuery, TopicDto>
    {
        private readonly ReadDbContext _readDbContext;
        private readonly IMapper _mapper;

        public GetParentTopicWithChildrenHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _readDbContext = readDbContext;
            _mapper = mapper;
        }

        public async Task<TopicDto> HandleAsync(GetParentTopicWithChildrenQuery query)
        {
            //Find all parents for topic with id: TopicId and select ONLY the root parent topic.
            //This is recursive function, that will be executed in the database.
            //The table and column names are written with double quotes, becouse there names must be interpreted by postgresql literally.
            //CAUTION: If the table and column names are written without double quotes there names will be lower cased by postgresql and the query will NOT be valid.

            var parentTopicReadModel =  await _readDbContext.Topics
                            .FromSqlRaw("WITH RECURSIVE ancestors AS (" +
                                        "   SELECT t.\"TopicId\", t.\"TopicName\", t.\"ParentTopicId\", t.\"Version\"\t\n" +
                                        "   FROM \"topic-article-service\".\"Topic\" AS t" +
                                        "   WHERE t.\"TopicId\" = @topicId" +
                                        "   UNION ALL" +
                                        "   SELECT t.\"TopicId\", t.\"TopicName\", t.\"ParentTopicId\", t.\"Version\"" +
                                        "   FROM \"topic-article-service\".\"Topic\" AS t" +
                                        "   JOIN ancestors ON t.\"TopicId\" = ancestors.\"ParentTopicId\"" +
                                        "   )" +
                                        "   SELECT * FROM ancestors",
                             new NpgsqlParameter("@topicId", query.TopicId)) //Include the TopicId. The documentation specifies that this is safer than $"{}".
                            .Where(x => x.Parent == null) //Include ONLY the root parent topic.
                            .FirstOrDefaultAsync();

            //Find all topics and include their children.
            var alltopics = _readDbContext.Topics
                .Include(x => x.Children)
                .ThenInclude(x => x.Children);

            //Find the topic, that has id equal to the id of the parent topic (parentTopicReadModel).
            var topicReadModel = await alltopics?.Where(x => x.TopicId == parentTopicReadModel.TopicId).FirstOrDefaultAsync();

            return _mapper.Map<TopicDto>(topicReadModel);
        }
    }
}
