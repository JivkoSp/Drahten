using UserService.Application.Dtos;

namespace UserService.Application.Queries
{
    public record GetReceivedContactRequestByUserQuery(Guid ReceiverUserId) : IQuery<List<ReceivedContactRequestByUserDto>>;
}
