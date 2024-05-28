using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.Repositories;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class AddUserHandler : ICommandHandler<AddUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IUserFactory _userFactory;

        public AddUserHandler(IUserRepository userRepository, IUserReadService userReadService, IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _userReadService = userReadService;
            _userFactory = userFactory;
        }

        public async Task HandleAsync(AddUserCommand command)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(command.UserId);

            if (alreadyExists)
            {
                throw new UserAlreadyExistsException(command.UserId);
            }

            var user = _userFactory.Create(command.UserId);

            await _userRepository.AddUserAsync(user);
        }
    }
}
