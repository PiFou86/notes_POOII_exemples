namespace ExempleReservation.Application;

public sealed record CreerBrouillonReservationCommande(
    string NomClient,
    DateTimeOffset DatePrevue);
