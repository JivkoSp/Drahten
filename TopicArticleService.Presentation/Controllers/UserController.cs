using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Commands.Dispatcher;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Dispatcher;
using TopicArticleService.Presentation.Dtos;

namespace TopicArticleService.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("topic-article-service/users")]
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

        [HttpGet("articles/{ArticleId:guid}", Name = "GetUsersRelatedToArticle")]
        public async Task<ActionResult> GetUsersRelatedToArticle([FromRoute] GetUsersRelatedToArticleQuery getUsersRelatedToArticleQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getUsersRelatedToArticleQuery);

            _responseDto.Result = result;

            if(result.Count == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpPost("{UserId:guid}/articles/")]
        public async Task<ActionResult> RegisterUserArticle([FromBody] RegisterUserArticleCommand registerUserArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(registerUserArticleCommand);

            return CreatedAtAction(actionName: nameof(GetUsersRelatedToArticle), 
                routeValues: new { ArticleId = registerUserArticleCommand.ArticleId }, null);
        }

        [HttpPost("{UserId:guid}/topics/")]
        public async Task<ActionResult> RegisterUserTopic([FromBody] RegisterUserTopicCommand registerUserTopicCommand)
        {
            await _commandDispatcher.DispatchAsync(registerUserTopicCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserCommand registerUserCommand)
        {
            await _commandDispatcher.DispatchAsync(registerUserCommand);

            return Created(HttpContext.Request.Path, null);
        }
    }
}
