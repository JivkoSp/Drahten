using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GeViewedUsersQuery(Guid ViewerUserId) : IQuery<List<ViewedUserDto>>;
}
