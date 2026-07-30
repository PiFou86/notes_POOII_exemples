using IDepotReservationsApplication = ExempleReservation.Application.IDepotReservations;
using ReservationDomaine = ExempleReservation.Domaine.Reservation;

namespace ExempleReservation.Infrastructure;

public sealed class DepotReservationsMemoire : IDepotReservationsApplication
{
    private readonly Dictionary<Guid, ReservationDomaine> m_reservations = new();

    public void Ajouter(ReservationDomaine reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        m_reservations.Add(reservation.Id, reservation);
    }

    public ReservationDomaine? Obtenir(Guid id)
    {
        return m_reservations.GetValueOrDefault(id);
    }

    public void MAJ(ReservationDomaine reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        m_reservations[reservation.Id] = reservation;
    }
}
