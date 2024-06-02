using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
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
                searchedTopicDataDto.SearchedData, searchedTopicDataDto.DateTime.ToUtc());

            user.RemoveSearchedTopicData(searchedTopicData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
