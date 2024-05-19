using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Commands.Handlers
{
    internal sealed class AddSearchedTopicDataHandler : ICommandHandler<AddSearchedTopicDataCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddSearchedTopicDataHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(AddSearchedTopicDataCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new UserNotFoundException(command.UserId);
            }

            var searchedTopicData = new SearchedTopicData(command.TopicId, command.UserId, command.SearchedData, command.DateTime);

            user.AddSearchedTopicData(searchedTopicData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
