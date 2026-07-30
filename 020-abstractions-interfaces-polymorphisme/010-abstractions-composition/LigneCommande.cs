public sealed class LigneCommande
{
    public string Description { get; }
    public decimal Montant { get; }

    public LigneCommande(string description, decimal montant)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentOutOfRangeException.ThrowIfNegative(montant);

        Description = description.Trim();
        Montant = montant;
    }
}
