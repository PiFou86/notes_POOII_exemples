namespace ExempleReservation.Domaine;

public sealed class ResultatCreationReservation
{
    public Reservation? Reservation { get; }
    public CodeRefusReservation CodeRefus { get; }
    public bool EstAcceptee => Reservation is not null;

    private ResultatCreationReservation(
        Reservation? reservation,
        CodeRefusReservation codeRefus)
    {
        Reservation = reservation;
        CodeRefus = codeRefus;
    }

    public static ResultatCreationReservation Accepter(
        Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        return new ResultatCreationReservation(
            reservation,
            CodeRefusReservation.Aucun);
    }

    public static ResultatCreationReservation Refuser(
        CodeRefusReservation codeRefus)
    {
        if (!Enum.IsDefined(codeRefus)
            || codeRefus == CodeRefusReservation.Aucun)
        {
            throw new ArgumentOutOfRangeException(nameof(codeRefus));
        }

        return new ResultatCreationReservation(null, codeRefus);
    }
}
