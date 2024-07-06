using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Application.Commands.Handlers
{
    internal sealed class AddCommentedArticleHandler : ICommandHandler<AddCommentedArticleCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddCommentedArticleHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddCommentedArticleCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var commentedArticle = new CommentedArticle(command.ArticleId, command.UserId, command.ArticleComment, command.DateTime.ToUniversalTime());

            user.AddCommentedArticle(commentedArticle);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
