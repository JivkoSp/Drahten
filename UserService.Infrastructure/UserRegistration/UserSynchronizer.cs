using UserService.Application.Commands;
using UserService.Application.Commands.Dispatcher;
using UserService.Application.Services.ReadServices;

namespace UserService.Infrastructure.UserRegistration
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

        public async Task SynchronizeUserAsync(CreateUserCommand createUserCommand)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(createUserCommand.UserId);

            if (alreadyExists == false)
            {
                await _commandDispatcher.DispatchAsync(createUserCommand);
            }
        }
    }
}
