using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class SetUserRetentionDateTimeHandler : ICommandHandler<SetUserRetentionDateTimeCommand>
    {
        private readonly IUserRepository _userRepository;

        public SetUserRetentionDateTimeHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(SetUserRetentionDateTimeCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var userRetentionUntil = new UserRetentionUntil(command.DateTime.ToUtc());

            user.SetUserRetentionDateTime(userRetentionUntil);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
