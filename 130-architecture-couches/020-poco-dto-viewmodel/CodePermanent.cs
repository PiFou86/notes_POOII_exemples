public sealed record CodePermanent
{
    public string Valeur { get; }

    public CodePermanent(string valeur)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(valeur);
        Valeur = valeur.Trim().ToUpperInvariant();
    }
}
