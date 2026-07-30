using ExempleReservation.Application;

namespace ExempleReservation.Infrastructure;

public sealed class HorlogeSysteme : IHorloge
{
    public DateTimeOffset Maintenant => DateTimeOffset.Now;
}
