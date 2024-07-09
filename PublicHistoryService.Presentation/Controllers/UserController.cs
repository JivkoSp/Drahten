using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicHistoryService.Application.Commands;
using PublicHistoryService.Application.Commands.Dispatcher;
using PublicHistoryService.Application.Queries;
using PublicHistoryService.Application.Queries.Dispatcher;
using PublicHistoryService.Presentation.Dtos;

namespace PublicHistoryService.Presentation.Controllers
{
    //TODO: Uncomment the line below after the integration tests.
    //[Authorize]
    [ApiController]
    [Route("publichistory-service/users")]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ResponseDto _responseDto;

        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _responseDto = new ResponseDto();
        }

        [HttpGet("{UserId:guid}/commented-articles/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetCommentedArticles([FromRoute] GetCommentedArticlesQuery getCommentedArticlesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getCommentedArticlesQuery);

            _responseDto.Result = result;

            if (result.Count() == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        //TODO: Change the name of the query. The query name should be GetSearchedArticlesQuery, instead of GetSearchedArticlesDataQuery.
        [HttpGet("{UserId:guid}/searched-articles/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetSearchedArticles([FromRoute] GetSearchedArticlesDataQuery getSearchedArticlesDataQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getSearchedArticlesDataQuery);

            _responseDto.Result = result;

            if (result.Count() == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{UserId:guid}/searched-topics/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetSearchedTopics([FromRoute] GetSearchedTopicsDataQuery getSearchedTopicsDataQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getSearchedTopicsDataQuery);

            _responseDto.Result = result;

            if (result.Count() == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{UserId:guid}/viewed-articles/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetViewedArticles([FromRoute] GetViewedArticlesQuery getViewedArticlesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getViewedArticlesQuery);

            _responseDto.Result = result;

            if (result.Count() == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{ViewerUserId:guid}/viewed-users/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetViewedUsers([FromRoute] GeViewedUsersQuery geViewedUsersQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(geViewedUsersQuery);

            _responseDto.Result = result;

            if (result.Count() == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpPost("{UserId:guid}")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        public async Task<ActionResult> RegisterUser([FromBody] AddUserCommand addUserCommand)
        {
            await _commandDispatcher.DispatchAsync(addUserCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{UserId:guid}/commented-articles/{ArticleId:guid}")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        public async Task<ActionResult> AddCommentedArticle([FromBody] AddCommentedArticleCommand addCommentedArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(addCommentedArticleCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{UserId:guid}/searched-articles/{ArticleId:guid}")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        public async Task<ActionResult> AddSearchedArticleData([FromBody] AddSearchedArticleDataCommand addSearchedArticleDataCommand)
        {
            await _commandDispatcher.DispatchAsync(addSearchedArticleDataCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{UserId:guid}/searched-topics/{TopicId:guid}")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        public async Task<ActionResult> AddSearchedTopicData([FromBody] AddSearchedTopicDataCommand addSearchedTopicDataCommand)
        {
            await _commandDispatcher.DispatchAsync(addSearchedTopicDataCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{UserId:guid}/viewed-articles/{ArticleId:guid}")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        public async Task<ActionResult> AddViewedArticle([FromBody] AddViewedArticleCommand addViewedArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(addViewedArticleCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{ViewerUserId:guid}/viewed-users/{ViewedUserId:guid}")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        public async Task<ActionResult> AddViewedUser([FromBody] AddViewedUserCommand addViewedUserCommand)
        {
            await _commandDispatcher.DispatchAsync(addViewedUserCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpDelete("{UserId:guid}/commented-articles/{CommentedArticleId:guid}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> RemoveCommentedArticle([FromRoute] RemoveCommentedArticleCommand removeCommentedArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(removeCommentedArticleCommand);

            return NoContent();
        }

        [HttpDelete("{UserId:guid}/searched-articles/{SearchedArticleDataId:guid}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> RemoveSearchedArticleData([FromRoute] RemoveSearchedArticleDataCommand removeSearchedArticleDataCommand)
        {
            await _commandDispatcher.DispatchAsync(removeSearchedArticleDataCommand);

            return NoContent();
        }

        [HttpDelete("{UserId:guid}/searched-topics/{SearchedTopicDataId:guid}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> RemoveSearchedTopicData([FromRoute] RemoveSearchedTopicDataCommand removeSearchedTopicDataCommand)
        {
            await _commandDispatcher.DispatchAsync(removeSearchedTopicDataCommand);

            return NoContent();
        }

        [HttpDelete("{UserId:guid}/viewed-articles/{ViewedArticleId:guid}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> RemoveViewedArticle([FromRoute] RemoveViewedArticleCommand removeViewedArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(removeViewedArticleCommand);

            return NoContent();
        }

        [HttpDelete("{ViewerUserId:guid}/viewed-users/{ViewedUserId:guid}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> RemoveViewedUser([FromRoute] RemoveViewedUserCommand removeViewedUserCommand)
        {
            await _commandDispatcher.DispatchAsync(removeViewedUserCommand);

            return NoContent();
        }
    }
}
