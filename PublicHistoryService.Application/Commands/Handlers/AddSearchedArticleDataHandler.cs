using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Application.Commands.Handlers
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

            var searchedArticleData = new SearchedArticleData(command.ArticleId, command.UserId, command.SearchedData,
                command.DateTime.ToUniversalTime());

            user.AddSearchedArticleData(searchedArticleData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
