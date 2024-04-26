using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Commands.Dispatcher;
using TopicArticleService.Application.Services.ReadServices;

namespace TopicArticleService.Infrastructure.UserRegistration
{
    public sealed class UserSynchronizer : IUserSynchronizer
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IUserReadService _userReadService;

        public UserSynchronizer(ICommandDispatcher commandDispatcher, IUserReadService userReadService)
        {
            _commandDispatcher = commandDispatcher;
            _userReadService = userReadService;
        }

        public async Task SynchronizeUserAsync(RegisterUserCommand registerUserCommand)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(registerUserCommand.UserId);

            if (alreadyExists == false)
            {
                await _commandDispatcher.DispatchAsync(registerUserCommand);
            }
        }
    }
}
