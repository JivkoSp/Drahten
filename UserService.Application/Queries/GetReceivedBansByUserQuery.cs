using UserService.Application.Dtos;

namespace UserService.Application.Queries
{
    public record GetReceivedBansByUserQuery(Guid ReceiverUserId) : IQuery<List<ReceivedBanByUserDto>>;
}
