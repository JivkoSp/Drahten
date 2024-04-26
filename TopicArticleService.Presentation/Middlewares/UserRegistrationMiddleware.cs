using System.Security.Claims;
using TopicArticleService.Application.Commands;
using TopicArticleService.Infrastructure.UserRegistration;

namespace TopicArticleService.Presentation.Middlewares
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

            var registerUserCommand = new RegisterUserCommand(userId);

            //Register the user in the database if it does not already exist.
            await _userSynchronizer.SynchronizeUserAsync(registerUserCommand);

            await next(context);
        }
    }
}
