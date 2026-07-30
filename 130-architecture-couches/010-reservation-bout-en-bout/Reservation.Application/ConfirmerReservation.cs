using ExempleReservation.Domaine;

namespace ExempleReservation.Application;

public sealed class ConfirmerReservation
{
    private readonly IDepotReservations m_depot;
    private readonly IFournisseurPolitiqueReservation m_fournisseurPolitique;

    public ConfirmerReservation(
        IDepotReservations depot,
        IFournisseurPolitiqueReservation fournisseurPolitique)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(fournisseurPolitique);
        m_depot = depot;
        m_fournisseurPolitique = fournisseurPolitique;
    }

    public Result<ReservationDto> Executer(
        ConfirmerReservationCommande commande)
    {
        ArgumentNullException.ThrowIfNull(commande);

        Reservation? reservation = m_depot.Obtenir(commande.ReservationId);
        if (reservation is null)
        {
            return Result<ReservationDto>.Echec(
                "La réservation est introuvable.");
        }

        DecisionReservation decision = reservation.Confirmer(
            m_fournisseurPolitique.Obtenir());

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
