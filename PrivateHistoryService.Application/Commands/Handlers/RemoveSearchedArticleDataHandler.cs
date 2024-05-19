using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveSearchedArticleDataHandler : ICommandHandler<RemoveSearchedArticleDataCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemoveSearchedArticleDataHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(RemoveSearchedArticleDataCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var searchedArticleData = new SearchedArticleData(command.ArticleId, command.UserId, command.SearchedData, command.DateTime);

            user.RemoveSearchedArticleData(searchedArticleData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
