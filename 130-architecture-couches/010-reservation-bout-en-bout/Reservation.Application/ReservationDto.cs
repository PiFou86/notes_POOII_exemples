namespace ExempleReservation.Application;

public sealed record ReservationDto(
    Guid Id,
    string NomClient,
    DateTimeOffset DatePrevue,
    string Etat,
    IReadOnlyList<ParticipantDto> Participants,
    IReadOnlyList<TarificationParticipantDto> Tarifications,
    decimal? Total);
