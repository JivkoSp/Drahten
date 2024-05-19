using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class AddLikedArticleHandler : ICommandHandler<AddLikedArticleCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddLikedArticleHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddLikedArticleCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var likedArticle = new LikedArticle(command.ArticleId, command.UserId, command.DateTime);

            user.AddLikedArticle(likedArticle); 

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
