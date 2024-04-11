using Microsoft.AspNetCore.Mvc;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Commands.Dispatcher;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Dispatcher;

namespace TopicArticleService.Presentation.Controllers
{
    [ApiController]
    [Route("topic-article-service/users")]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("articles/{ArticleId:guid}")]
        public async Task<ActionResult> GetUsersRelatedToArticle([FromRoute] GetUsersRelatedToArticleQuery getUsersRelatedToArticleQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getUsersRelatedToArticleQuery);

            if(result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("{UserId:guid}/articles/")]
        public async Task<ActionResult> RegisterUserArticle([FromBody] RegisterUserArticleCommand registerUserArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(registerUserArticleCommand);

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
