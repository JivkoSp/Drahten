using System.Security.Claims;
using UserService.Application.Commands;
using UserService.Infrastructure.UserRegistration;

namespace UserService.Presentation.Middlewares
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
            var userFullName = context.User.FindFirstValue("name");
            var userNickName = context.User.FindFirstValue("preferred_username");
            var userEmailAddress = context.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");

            var createUserCommand = new CreateUserCommand(userId, userFullName, userNickName, userEmailAddress);

            //Register the user in the database if it does not already exist.
            await _userSynchronizer.SynchronizeUserAsync(createUserCommand);

            await next(context);
        }
    }
}
