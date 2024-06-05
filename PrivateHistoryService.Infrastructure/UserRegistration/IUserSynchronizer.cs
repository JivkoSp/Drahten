using PrivateHistoryService.Application.Commands;

namespace PrivateHistoryService.Infrastructure.UserRegistration
{
    public interface IUserSynchronizer
    {
        Task SynchronizeUserAsync(AddUserCommand addUserCommand);
    }
}
