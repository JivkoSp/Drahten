using PublicHistoryService.Application.Commands;
using PublicHistoryService.Infrastructure.UserRegistration;
using System.Security.Claims;

namespace PublicHistoryService.Presentation.Middlewares
{
    public sealed class UserRegistrationMiddleware : IMiddleware
    {
        private readonly IUserSynchronizer _userSynchronizer;

        public UserRegistrationMiddleware(IUserSynchronizer userSynchronizer)
        {
            _userSynchronizer = userSynchronizer;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var userId = Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var addUserCommand = new AddUserCommand(userId);

            //Register the user in the database if it does not already exist.
            await _userSynchronizer.SynchronizeUserAsync(addUserCommand);

            await next(context);
        }
    }
}
