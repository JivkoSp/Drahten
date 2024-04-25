using UserService.Application.Dtos;

namespace UserService.Application.AsyncDataServices
{
    public interface IMessageBusPublisher
    {
        void PublishNewUser(UserPublishedDto userPublishedDto);
    }
}
