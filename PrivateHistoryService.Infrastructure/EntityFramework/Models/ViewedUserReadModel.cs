
namespace PrivateHistoryService.Infrastructure.EntityFramework.Models
{
    internal class ViewedUserReadModel
    {
        //Primary key
        public Guid ViewedUserId { get; set; }
        public string ViewerId { get; set; }
        public string ViewedId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel Viewer { get; set; }
        public virtual UserReadModel Viewed { get; set; }
    }
}
