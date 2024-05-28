using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class AddDislikedArticleHandler : ICommandHandler<AddDislikedArticleCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddDislikedArticleHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddDislikedArticleCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var dislikedArticle = new DislikedArticle(command.ArticleId, command.UserId, command.DateTime.ToUtc());

            user.AddDislikedArticle(dislikedArticle);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
