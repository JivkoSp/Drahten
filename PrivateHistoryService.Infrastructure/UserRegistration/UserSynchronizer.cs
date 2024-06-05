using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Dispatcher;
using PrivateHistoryService.Application.Services.ReadServices;

namespace PrivateHistoryService.Infrastructure.UserRegistration
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

        public async Task SynchronizeUserAsync(AddUserCommand addUserCommand)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(addUserCommand.UserId);

            if (alreadyExists == false)
            {
                await _commandDispatcher.DispatchAsync(addUserCommand);
            }
        }
    }
}
