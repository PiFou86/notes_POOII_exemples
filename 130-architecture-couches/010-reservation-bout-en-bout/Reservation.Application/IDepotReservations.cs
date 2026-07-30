using ReservationDomaine = ExempleReservation.Domaine.Reservation;

namespace ExempleReservation.Application;

public interface IDepotReservations
{
    void Ajouter(ReservationDomaine reservation);
    ReservationDomaine? Obtenir(Guid id);
    void MAJ(ReservationDomaine reservation);
}
