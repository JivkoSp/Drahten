using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class AddSearchedArticleDataHandler : ICommandHandler<AddSearchedArticleDataCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddSearchedArticleDataHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddSearchedArticleDataCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var searchedArticleData = new SearchedArticleData(command.ArticleId, command.UserId, command.SearchedData, command.DateTime.ToUtc());

            user.AddSearchedArticleData(searchedArticleData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
