using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveViewedArticleHandler : ICommandHandler<RemoveViewedArticleCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveViewedArticleHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(RemoveViewedArticleCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var viewedArticle = new ViewedArticle(command.ArticleId, command.UserId, command.DateTime);

            user.RemoveViewedArticle(viewedArticle);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
