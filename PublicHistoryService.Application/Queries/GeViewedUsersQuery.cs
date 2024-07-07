using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Queries
{
    public record GeViewedUsersQuery(Guid ViewerUserId) : IQuery<List<ViewedUserDto>>;
}
