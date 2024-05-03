using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;

namespace TopicArticleService.Application.Commands.Handlers
{
    internal sealed class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserReadService _userReadService;
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public RegisterUserHandler(IUserReadService userReadService, IUserRepository userRepository, IUserFactory userFactory)
        {
            _userReadService = userReadService;
            _userRepository = userRepository;
            _userFactory = userFactory; 
        }

        public async Task HandleAsync(RegisterUserCommand command)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(command.UserId);

            if(alreadyExists)
            {
                throw new UserAlreadyExistsException(command.UserId);
            }

            var user = _userFactory.Create(command.UserId);

            await _userRepository.AddUserAsync(user);
        }
    }
}
