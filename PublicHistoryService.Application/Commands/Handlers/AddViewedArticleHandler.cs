using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Application.Commands.Handlers
{
    internal sealed class AddViewedArticleHandler : ICommandHandler<AddViewedArticleCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddViewedArticleHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddViewedArticleCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var viewedArticle = new ViewedArticle(command.ArticleId, command.UserId, command.DateTime.ToUniversalTime());

            user.AddViewedArticle(viewedArticle);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
