using ExempleReservation.Domaine;

namespace ExempleReservation.Application;

public interface IFournisseurPolitiqueReservation
{
    PolitiqueReservation Obtenir();
}
