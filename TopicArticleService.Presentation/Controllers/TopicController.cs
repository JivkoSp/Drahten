using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Dispatcher;
using TopicArticleService.Presentation.Dtos;

namespace TopicArticleService.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("topic-article-service/topics")]
    public class TopicController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ResponseDto _responseDto;

        public TopicController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetTopics([FromRoute] GetTopicsQuery getTopicsQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getTopicsQuery);

            _responseDto.Result = result;

            if (result.Count == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{TopicId:guid}/parent-topic/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetRootTopicWithChildren([FromRoute] GetParentTopicWithChildrenQuery getParentTopicWithChildrenQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getParentTopicWithChildrenQuery);

            _responseDto.Result = result;

            if(result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{UserId:guid}/user-topics/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetTopicsRelatedToUser([FromRoute] GetTopicsRelatedToUserQuery getTopicsRelatedToUserQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getTopicsRelatedToUserQuery);

            _responseDto.Result = result;

            if (result.Count == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }
    }
}
