using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Factories.Interfaces
{
    public interface IUserFactory
    {
        User Create(UserID userId);
    }
}
