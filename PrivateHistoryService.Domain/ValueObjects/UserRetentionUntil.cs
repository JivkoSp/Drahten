using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record UserRetentionUntil
    {
        internal DateTimeOffset RetentionUntil { get; set; } // Date until which data should be retained

        public UserRetentionUntil(DateTimeOffset dateTime)
        {
            if (dateTime == default || dateTime < DateTimeOffset.Now)
            {
                throw new InvalidUserRetentionUntilDateTimeException();
            }

            RetentionUntil = dateTime;
        }

        //Conversion from ValueObject to DateTimeOffset.
        public static implicit operator DateTimeOffset(UserRetentionUntil userRetentionUntil)
            => userRetentionUntil.RetentionUntil;

        //Conversion from DateTimeOffset to ValueObject.
        public static implicit operator UserRetentionUntil(DateTimeOffset dateTime)
            => new UserRetentionUntil(dateTime);
    }
}
