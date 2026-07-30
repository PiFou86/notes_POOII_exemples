namespace ExempleReservation.Domaine;

public enum CodeRefusReservation
{
    Aucun,
    DateNonFuture,
    NomParticipantObligatoire,
    DateNaissanceApresReservation,
    ReservationConfirmee,
    ParticipantDuplique,
    CapaciteAtteinte,
    ParticipantIntrouvable,
    ReservationVide
}

public sealed record DecisionReservation
{
    public bool EstAcceptee { get; }
    public CodeRefusReservation CodeRefus { get; }

    private DecisionReservation(
        bool estAcceptee,
        CodeRefusReservation codeRefus)
    {
        EstAcceptee = estAcceptee;
        CodeRefus = codeRefus;
    }

    public static DecisionReservation Accepter()
    {
        return new DecisionReservation(true, CodeRefusReservation.Aucun);
    }

    public static DecisionReservation Refuser(CodeRefusReservation code)
    {
        if (!Enum.IsDefined(code) || code == CodeRefusReservation.Aucun)
        {
            throw new ArgumentOutOfRangeException(nameof(code));
        }

        return new DecisionReservation(false, code);
    }
}
