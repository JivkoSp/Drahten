using PublicHistoryService.Application.Exceptions;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Application.Commands.Handlers
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

            var searchedTopicData = new SearchedTopicData(command.TopicId, command.UserId, command.SearchedData, command.DateTime.ToUniversalTime());

            user.AddSearchedTopicData(searchedTopicData);

            await _userRepository.UpdateUserAsync(user);
        }
    }
}
