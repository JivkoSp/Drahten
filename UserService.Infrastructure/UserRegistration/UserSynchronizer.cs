using Microsoft.Extensions.Logging;
using UserService.Application.Commands;
using UserService.Application.Commands.Dispatcher;
using UserService.Application.Services.ReadServices;

namespace UserService.Infrastructure.UserRegistration
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

        public async Task SynchronizeUserAsync(CreateUserCommand createUserCommand)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(createUserCommand.UserId);

            if (alreadyExists == false)
            {
                _logger.LogInformation($"--> User with ID: {createUserCommand.UserId} is ATTEMPTING to synchronize with UserService.");

                await _commandDispatcher.DispatchAsync(createUserCommand);

                _logger.LogInformation($"--> User with ID: {createUserCommand.UserId} is SUCCESSFULLY synchronized with UserService.");
            }
        }
    }
}
