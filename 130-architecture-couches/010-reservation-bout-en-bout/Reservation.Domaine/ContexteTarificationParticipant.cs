namespace ExempleReservation.Domaine;

public sealed class ContexteTarificationParticipant
{
    public Participant Participant { get; }
    public DateOnly DateReservation { get; }
    public int AgeALaDate { get; }

    public ContexteTarificationParticipant(
        Participant participant,
        DateOnly dateReservation)
    {
        ArgumentNullException.ThrowIfNull(participant);
        Participant = participant;
        DateReservation = dateReservation;
        AgeALaDate = participant.CalculerAgeALaDate(dateReservation);
    }
}
