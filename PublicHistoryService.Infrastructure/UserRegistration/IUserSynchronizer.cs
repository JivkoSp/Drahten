using PublicHistoryService.Application.Commands;

namespace PublicHistoryService.Infrastructure.UserRegistration
{
    public interface IUserSynchronizer
    {
        Task SynchronizeUserAsync(AddUserCommand addUserCommand);
    }
}
