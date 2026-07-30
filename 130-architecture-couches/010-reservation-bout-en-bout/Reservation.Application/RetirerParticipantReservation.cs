using ExempleReservation.Domaine;

namespace ExempleReservation.Application;

public sealed class RetirerParticipantReservation
{
    private readonly IDepotReservations m_depot;

    public RetirerParticipantReservation(IDepotReservations depot)
    {
        ArgumentNullException.ThrowIfNull(depot);
        m_depot = depot;
    }

    public Result<ReservationDto> Executer(
        RetirerParticipantReservationCommande commande)
    {
        ArgumentNullException.ThrowIfNull(commande);

        Reservation? reservation = m_depot.Obtenir(commande.ReservationId);
        if (reservation is null)
        {
            return Result<ReservationDto>.Echec(
                "La réservation est introuvable.");
        }

        DecisionReservation decision = reservation.RetirerParticipant(
            commande.ParticipantId);

        if (!decision.EstAcceptee)
        {
            return Result<ReservationDto>.Echec(
                MessagesRefusReservation.Obtenir(decision.CodeRefus));
        }

        m_depot.MAJ(reservation);
        return Result<ReservationDto>.Succes(
            ConvertisseurReservationDto.VersDto(reservation));
    }
}
