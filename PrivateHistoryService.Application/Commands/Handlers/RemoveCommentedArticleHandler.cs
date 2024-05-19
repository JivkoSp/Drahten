using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveCommentedArticleHandler : ICommandHandler<RemoveCommentedArticleCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveCommentedArticleHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(RemoveCommentedArticleCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var commentedArticle = new CommentedArticle(command.ArticleId, command.UserId, command.ArticleComment, command.DateTime);

            user.RemoveCommentedArticle(commentedArticle);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
