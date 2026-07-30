public sealed class CalculateurTotal
{
    private const decimal TauxTaxes = 0.14975m;
    public decimal CalculerTotalAvecTaxes(decimal prix) => prix + prix * TauxTaxes;
}
