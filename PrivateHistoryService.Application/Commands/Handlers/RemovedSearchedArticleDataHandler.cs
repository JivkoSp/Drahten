using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemovedSearchedArticleDataHandler : ICommandHandler<RemovedSearchedArticleDataCommand>
    {
        private readonly IUserRepository _userRepository;

        public RemovedSearchedArticleDataHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(RemovedSearchedArticleDataCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var searchedArticleData = new SearchedArticleData(command.ArticleId, command.UserId, command.SearchedData, command.DateTime);

            user.RemovedSearchedArticleData(searchedArticleData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
