using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class AddDislikedArticleCommentHandler : ICommandHandler<AddDislikedArticleCommentCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddDislikedArticleCommentHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddDislikedArticleCommentCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var dislikedArticleComment = new DislikedArticleComment(command.ArticleId, command.UserId, command.ArticleCommentId, command.DateTime);

            user.AddDislikedArticleComment(dislikedArticleComment);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
