using E_proc.Services.Interfaces;

namespace E_proc.Services.Implementations
{
    public class DateService : IDateService
    {
        public long ConvertDatetimeToUnixTimeStamp(DateTime date)
        {
            var dateTimeOffset = new DateTimeOffset(date);
            var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
            return unixDateTime;
        }
    }
}
