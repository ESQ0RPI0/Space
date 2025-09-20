using Space.Server.Services.Interfaces.Common;

namespace Space.Server.Services.Common
{
    public sealed class DateTimeService : IDateTimeService
    {
        private readonly TimeProvider _timeProvider;

        public DateTimeService(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public DateTimeOffset GetCurrentDateTime()
        {
            return _timeProvider.GetUtcNow();
        }
    }
}
