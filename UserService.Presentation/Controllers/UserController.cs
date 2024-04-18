using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;
using UserService.Application.Commands.Dispatcher;
using UserService.Application.Queries;
using UserService.Application.Queries.Dispatcher;

namespace UserService.Presentation.Controllers
{
    [ApiController]
    [Route("user-service/users")]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{UserId:guid}")]
        public async Task<ActionResult> GetUser([FromRoute] GetUserQuery getUserQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getUserQuery);

            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpGet("{IssuerUserId:guid}/issued-bans-by-user/")]
        public async Task<ActionResult> GetIssuedBansByUser([FromRoute] GetIssuedBansByUserQuery getBannedUsersQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getBannedUsersQuery);

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{ReceiverUserId:guid}/received-bans-by-user/")]
        public async Task<ActionResult> GetReceivedBansByUser([FromRoute] GetReceivedBansByUserQuery getReceivedBansByUserQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getReceivedBansByUserQuery);

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{IssuerUserId:guid}/issued-contact-requests-by-user/")]
        public async Task<ActionResult> GetIssuedContactRequestsByUser(
            [FromRoute] GetIssuedContactRequestsByUserQuery getUserContactRequestsQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getUserContactRequestsQuery);

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{ReceiverUserId:guid}/received-contact-requests-by-user/")]
        public async Task<ActionResult> GetReceivedContactRequestsByUser(
            [FromRoute] GetReceivedContactRequestByUserQuery getReceivedContactRequestByUserQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getReceivedContactRequestByUserQuery);

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] CreateUserCommand createUserCommand)
        {
            await _commandDispatcher.DispatchAsync(createUserCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{IssuerUserId:guid}/banned-users/{ReceiverUserId:guid}")]
        public async Task<ActionResult> RegisterBannedUser([FromBody] BanUserCommand banUserCommand)
        {
            await _commandDispatcher.DispatchAsync(banUserCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{IssuerUserId:guid}/contact-requests/{ReceiverUserId:guid}")]
        public async Task<ActionResult> RegisterContactRequest([FromBody] AddContactRequestCommand addContactRequestCommand)
        {
            await _commandDispatcher.DispatchAsync(addContactRequestCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{UserId:guid}/user-tracking")]
        public async Task<ActionResult> RegisterUserActivity([FromBody] AddToAuditTrailCommand addToAuditTrailCommand)
        {
            await _commandDispatcher.DispatchAsync(addToAuditTrailCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPut("{IssuerUserId:guid}/update-contact-request-message/{ReceiverUserId:guid}")]
        public async Task<ActionResult> UpdateContactRequestMessage([FromBody] UpdateContactRequestMessageCommand updateContactRequestMessage)
        {
            await _commandDispatcher.DispatchAsync(updateContactRequestMessage);

            return NoContent();
        }

        [HttpDelete("{IssuerUserId:guid}/issued-contact-requests/{ReceiverUserId:guid}")]
        public async Task<ActionResult> RemoveIssuedContactRequest([FromRoute] RemoveIssuedContactRequestCommand removeContactRequestCommand)
        {
            await _commandDispatcher.DispatchAsync(removeContactRequestCommand);

            return NoContent();
        }

        [HttpDelete("{ReceiverUserId:guid}/received-contact-requests/{IssuerUserId:guid}")]
        public async Task<ActionResult> RemoveReceivedContactRequest([FromRoute] RemoveReceivedContactRequestCommand removeReceivedContactRequestCommand)
        {
            await _commandDispatcher.DispatchAsync(removeReceivedContactRequestCommand);

            return NoContent();
        }

        [HttpDelete("{IssuerUserId:guid}/banned-users/{ReceiverUserId:guid}")]
        public async Task<ActionResult> RemoveBannedUser([FromRoute] UnbanUserCommand unbanUserCommand)
        {
            await _commandDispatcher.DispatchAsync(unbanUserCommand);

            return NoContent();
        }
    }
}
