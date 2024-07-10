using Microsoft.Extensions.Logging;
using PublicHistoryService.Application.Commands;
using PublicHistoryService.Application.Commands.Dispatcher;
using PublicHistoryService.Application.Services.ReadServices;

namespace PublicHistoryService.Infrastructure.UserRegistration
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

        public async Task SynchronizeUserAsync(AddUserCommand addUserCommand)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(addUserCommand.UserId);

            if (alreadyExists == false)
            {
                _logger.LogInformation($"--> User with ID: {addUserCommand.UserId} is ATTEMPTING to synchronize with PublicHistoryService.");

                await _commandDispatcher.DispatchAsync(addUserCommand);

                _logger.LogInformation($"--> User with ID: {addUserCommand.UserId} is SUCCESSFULLY synchronized with PublicHistoryService.");
            }
        }
    }
}
