
namespace PublicHistoryService.Infrastructure.EntityFramework.Models
{
    internal class ViewedUserReadModel
    {
        //Primary key
        public Guid ViewedUserReadModelId { get; set; }
        public string ViewerUserId { get; set; }
        public string ViewedUserId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel ViewedUser { get; set; }
    }
}
