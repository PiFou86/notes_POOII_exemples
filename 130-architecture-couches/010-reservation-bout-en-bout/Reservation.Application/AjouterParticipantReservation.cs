using ExempleReservation.Domaine;

namespace ExempleReservation.Application;

public sealed class AjouterParticipantReservation
{
    private readonly IDepotReservations m_depot;
    private readonly IFournisseurPolitiqueReservation m_fournisseurPolitique;

    public AjouterParticipantReservation(
        IDepotReservations depot,
        IFournisseurPolitiqueReservation fournisseurPolitique)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(fournisseurPolitique);
        m_depot = depot;
        m_fournisseurPolitique = fournisseurPolitique;
    }

    public Result<ReservationDto> Executer(
        AjouterParticipantReservationCommande commande)
    {
        ArgumentNullException.ThrowIfNull(commande);

        if (string.IsNullOrWhiteSpace(commande.Nom))
        {
            return Result<ReservationDto>.Echec(
                "Le nom du participant est obligatoire.");
        }

        Reservation? reservation = m_depot.Obtenir(commande.ReservationId);
        if (reservation is null)
        {
            return Result<ReservationDto>.Echec(
                "La réservation est introuvable.");
        }

        Participant participant = new(
            Guid.NewGuid(),
            commande.Nom,
            commande.DateNaissance,
            commande.EstEtudiant);

        DecisionReservation decision = reservation.AjouterParticipant(
            participant,
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
