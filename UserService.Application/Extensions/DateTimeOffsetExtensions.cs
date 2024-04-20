
namespace UserService.Application.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset ToUtc(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToUniversalTime();
        }
    }
}
