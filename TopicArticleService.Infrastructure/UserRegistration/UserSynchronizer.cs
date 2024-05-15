using Microsoft.Extensions.Logging;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Commands.Dispatcher;
using TopicArticleService.Application.Services.ReadServices;

namespace TopicArticleService.Infrastructure.UserRegistration
{
    public sealed class UserSynchronizer : IUserSynchronizer
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IUserReadService _userReadService;
        private readonly ILogger<UserSynchronizer> _logger;

        public UserSynchronizer(ICommandDispatcher commandDispatcher, IUserReadService userReadService, ILogger<UserSynchronizer> logger)
        {
            _commandDispatcher = commandDispatcher;
            _userReadService = userReadService;
            _logger = logger;
        }

        public async Task SynchronizeUserAsync(RegisterUserCommand registerUserCommand)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(registerUserCommand.UserId);

            if (alreadyExists == false)
            {
                _logger.LogInformation($"--> User with ID: {registerUserCommand.UserId} is ATTEMPTING to synchronize with TopicArticleService.");

                await _commandDispatcher.DispatchAsync(registerUserCommand);

                _logger.LogInformation($"--> User with ID: {registerUserCommand.UserId} is SUCCESSFULLY synchronized with TopicArticleService.");
            }
        }
    }
}
