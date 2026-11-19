using ExempleReservation.Application;

internal sealed class FausseHorloge : IHorloge
{
    public DateTimeOffset Maintenant { get; }

    public FausseHorloge(DateTimeOffset maintenant)
    {
        Maintenant = maintenant;
    }
}
