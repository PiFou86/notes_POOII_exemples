namespace ExempleReservation.Application;

public sealed record TarificationParticipantDto(
    Guid ParticipantId,
    int Age,
    IReadOnlyList<string> CodesAdmissibles,
    string CodeRabaisRetenu,
    decimal PourcentageRabais,
    decimal TarifRegulier,
    decimal TarifFinal);
