using Microsoft.AspNetCore.Mvc;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Dispatcher;

namespace TopicArticleService.Presentation.Controllers
{
    [ApiController]
    [Route("topic-article-service/topics")]
    public class TopicController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public TopicController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetTopics([FromRoute] GetTopicsQuery getTopicsQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getTopicsQuery);

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{TopicId:guid}/parent-topic/")]
        public async Task<ActionResult> GetRootTopicWithChildren([FromRoute] GetParentTopicWithChildrenQuery getParentTopicWithChildrenQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getParentTopicWithChildrenQuery);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{UserId:guid}/user-topics/")]
        public async Task<ActionResult> GetTopicsRelatedToUser([FromRoute] GetTopicsRelatedToUserQuery getTopicsRelatedToUserQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getTopicsRelatedToUserQuery);

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
