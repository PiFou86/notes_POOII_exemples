namespace ExempleReservation.Application;

public sealed record AjouterParticipantReservationCommande(
    Guid ReservationId,
    string Nom,
    DateOnly DateNaissance,
    bool EstEtudiant);
