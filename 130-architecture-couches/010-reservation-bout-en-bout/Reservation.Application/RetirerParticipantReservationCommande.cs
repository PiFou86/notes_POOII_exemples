namespace ExempleReservation.Application;

public sealed record RetirerParticipantReservationCommande(
    Guid ReservationId,
    Guid ParticipantId);
