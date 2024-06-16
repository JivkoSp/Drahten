using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveSearchedArticleDataHandler : ICommandHandler<RemoveSearchedArticleDataCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISearchedArticleDataReadService _searchedArticleDataReadService;

        public RemoveSearchedArticleDataHandler(IUserRepository userRepository, ISearchedArticleDataReadService searchedArticleDataReadService)
        {
            _userRepository = userRepository;
            _searchedArticleDataReadService = searchedArticleDataReadService;
        }

        public async Task HandleAsync(RemoveSearchedArticleDataCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var searchedArticleDataDto = await _searchedArticleDataReadService.GetSearchedArticleDataByIdAsync(command.SearchedArticleDataId);

            if (searchedArticleDataDto == null)
            {
                throw new SearchedArticleNotFoundException(command.SearchedArticleDataId);
            }

            var searchedArticleData = new SearchedArticleData(Guid.Parse(searchedArticleDataDto.ArticleId), Guid.Parse(searchedArticleDataDto.UserId),
                 searchedArticleDataDto.SearchedData, searchedArticleDataDto.SearchedDataAnswer, searchedArticleDataDto.DateTime.ToUtc());

            user.RemoveSearchedArticleData(searchedArticleData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
