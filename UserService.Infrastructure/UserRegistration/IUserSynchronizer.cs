using UserService.Application.Commands;

namespace UserService.Infrastructure.UserRegistration
{
    public interface IUserSynchronizer
    {
        Task SynchronizeUserAsync(CreateUserCommand createUserCommand);
    }
}
