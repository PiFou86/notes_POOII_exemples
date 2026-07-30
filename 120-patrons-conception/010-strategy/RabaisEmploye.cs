public sealed class RabaisEmploye : IRabaisStrategy
{
    public decimal CalculerRabais(decimal montant) => montant * 0.25m;
}
