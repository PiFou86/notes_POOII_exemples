using ExempleReservation.Domaine;

namespace ExempleReservation.Application;

internal static class ConvertisseurReservationDto
{
    public static ReservationDto VersDto(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);

        return new ReservationDto(
            reservation.Id,
            reservation.NomClient,
            reservation.DatePrevue,
            reservation.Etat.ToString(),
            reservation.Participants.Select(participant =>
                new ParticipantDto(
                    participant.Id,
                    participant.Nom,
                    participant.DateNaissance,
                    participant.EstEtudiant)).ToList(),
            reservation.Tarifications.Select(tarification =>
                new TarificationParticipantDto(
                    tarification.ParticipantId,
                    tarification.Age,
                    tarification.CodesAdmissibles,
                    tarification.RabaisRetenu.Code,
                    tarification.RabaisRetenu.Pourcentage.Valeur,
                    tarification.TarifRegulier.Valeur,
                    tarification.TarifFinal.Valeur)).ToList(),
            reservation.Total?.Valeur);
    }
}
