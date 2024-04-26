using TopicArticleService.Application.Commands;

namespace TopicArticleService.Infrastructure.UserRegistration
{
    public interface IUserSynchronizer
    {
        Task SynchronizeUserAsync(RegisterUserCommand registerUserCommand);
    }
}
