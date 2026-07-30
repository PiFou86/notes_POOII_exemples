using ExempleReservation.Domaine;

namespace ExempleReservation.Application;

public sealed class CreerBrouillonReservation
{
    private readonly IDepotReservations m_depot;
    private readonly IHorloge m_horloge;

    public CreerBrouillonReservation(
        IDepotReservations depot,
        IHorloge horloge)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(horloge);
        m_depot = depot;
        m_horloge = horloge;
    }

    public Result<ReservationDto> Executer(
        CreerBrouillonReservationCommande commande)
    {
        ArgumentNullException.ThrowIfNull(commande);

        if (string.IsNullOrWhiteSpace(commande.NomClient))
        {
            return Result<ReservationDto>.Echec(
                "Le nom du client est obligatoire.");
        }

        ResultatCreationReservation resultat = Reservation.CreerBrouillon(
            Guid.NewGuid(),
            commande.NomClient,
            commande.DatePrevue,
            m_horloge.Maintenant);

        if (resultat.Reservation is not Reservation reservation)
        {
            return Result<ReservationDto>.Echec(
                MessagesRefusReservation.Obtenir(resultat.CodeRefus));
        }

        m_depot.Ajouter(reservation);
        return Result<ReservationDto>.Succes(
            ConvertisseurReservationDto.VersDto(reservation));
    }
}
