using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Application.Commands.Handlers
{
    internal sealed class RemoveSearchedTopicDataHandler : ICommandHandler<RemoveSearchedTopicDataCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISearchedTopicDataReadService _searchedTopicDataReadService;

        public RemoveSearchedTopicDataHandler(IUserRepository userRepository, ISearchedTopicDataReadService searchedTopicDataReadService)
        {
            _userRepository = userRepository;
            _searchedTopicDataReadService = searchedTopicDataReadService;
        }

        public async Task HandleAsync(RemoveSearchedTopicDataCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var searchedTopicDataDto = await _searchedTopicDataReadService.GetSearchedTopicDataByIdAsync(command.SearchedTopicDataId);

            if (searchedTopicDataDto == null)
            {
                throw new SearchedTopicNotFoundException(command.SearchedTopicDataId);
            }

            var searchedTopicData = new SearchedTopicData(searchedTopicDataDto.TopicId, Guid.Parse(searchedTopicDataDto.UserId),
                searchedTopicDataDto.SearchedData, searchedTopicDataDto.DateTime.ToUniversalTime());

            user.RemoveSearchedTopicData(searchedTopicData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
