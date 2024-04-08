using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Application.Services.WriteServices;

namespace TopicArticleService.Application.Commands.Handlers
{
    public sealed class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserReadService _userReadService;
        private readonly IUserWriteService _userWriteService;

        public RegisterUserHandler(IUserReadService userReadService, IUserWriteService userWriteService)
        {
            _userReadService = userReadService;
            _userWriteService = userWriteService;
        }

        public async Task HandleAsync(RegisterUserCommand command)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(command.UserId);

            if(alreadyExists)
            {
                //There is user with command.UserId.

                throw new UserAlreadyExistsException(command.UserId);
            }

            await _userWriteService.AddUserAsync(command.UserId);
        }
    }
}
