
namespace UserService.Application.Commands
{
    public record CreateUserCommand(Guid UserId, string UserFullName, string UserNickName, string UserEmailAddress) : ICommand;
}
