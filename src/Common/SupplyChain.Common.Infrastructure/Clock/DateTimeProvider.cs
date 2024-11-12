
using SupplyChain.Common.Application.Clock;

namespace SupplyChain.Common.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
