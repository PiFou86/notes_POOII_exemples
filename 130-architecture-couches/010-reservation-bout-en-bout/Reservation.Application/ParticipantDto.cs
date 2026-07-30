namespace ExempleReservation.Application;

public sealed record ParticipantDto(
    Guid Id,
    string Nom,
    DateOnly DateNaissance,
    bool EstEtudiant);
