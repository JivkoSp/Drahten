namespace Drahten_Services_UserService.Dtos
{
    public class WriteUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public int TopicId { get; set; }
    }
}
