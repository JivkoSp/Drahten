using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class AddLikedArticleCommentHandler : ICommandHandler<AddLikedArticleCommentCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddLikedArticleCommentHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddLikedArticleCommentCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var likedArticleComment = new LikedArticleComment(command.ArticleId, command.UserId, command.ArticleCommentId, command.DateTime);

            user.AddLikedArticleComment(likedArticleComment);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
