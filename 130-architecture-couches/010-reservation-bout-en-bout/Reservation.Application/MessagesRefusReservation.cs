using ExempleReservation.Domaine;

namespace ExempleReservation.Application;

internal static class MessagesRefusReservation
{
    public static string Obtenir(CodeRefusReservation code)
    {
        return code switch
        {
            CodeRefusReservation.DateNonFuture =>
                "La réservation doit être prévue dans le futur.",
            CodeRefusReservation.NomParticipantObligatoire =>
                "Le nom du participant est obligatoire.",
            CodeRefusReservation.DateNaissanceApresReservation =>
                "La date de naissance doit précéder la réservation.",
            CodeRefusReservation.ReservationConfirmee =>
                "Une réservation confirmée ne peut plus être modifiée.",
            CodeRefusReservation.ParticipantDuplique =>
                "Ce participant figure déjà dans la réservation.",
            CodeRefusReservation.CapaciteAtteinte =>
                "La réservation a atteint le nombre maximal de participants.",
            CodeRefusReservation.ParticipantIntrouvable =>
                "Le participant est introuvable.",
            CodeRefusReservation.ReservationVide =>
                "Une réservation vide ne peut pas être confirmée.",
            _ => "La réservation a été refusée."
        };
    }
}
