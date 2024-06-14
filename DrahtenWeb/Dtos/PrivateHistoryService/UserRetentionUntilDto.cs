namespace DrahtenWeb.Dtos.PrivateHistoryService
{
    public class UserRetentionUntilDto
    {
       public Guid UserId { get; set; }
       public DateTimeOffset DateTime {  get; set; }
    }
}
