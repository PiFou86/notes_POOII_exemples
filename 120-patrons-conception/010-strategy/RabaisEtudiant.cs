public sealed class RabaisEtudiant : IRabaisStrategy
{
    public decimal CalculerRabais(decimal montant) => montant * 0.15m;
}
