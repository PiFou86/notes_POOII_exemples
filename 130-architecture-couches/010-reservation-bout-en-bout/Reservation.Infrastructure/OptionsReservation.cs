namespace ExempleReservation.Infrastructure;

public sealed class OptionsReservation
{
    public int NombreParticipantsMaximum { get; set; }
    public decimal TarifRegulierParPersonne { get; set; }
    public int AgeMaximumEnfant { get; set; }
    public int AgeMinimumAine { get; set; }
    public OptionsRabaisPublic RabaisPublic { get; set; } = new();
    public List<OptionsPromotion> PromotionsActives { get; set; } = new();
}
