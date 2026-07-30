public sealed record Courriel
{
    public string Valeur { get; }

    public Courriel(string valeur)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(valeur);
        string normalisee = valeur.Trim().ToLowerInvariant();
        if (!normalisee.Contains('@', StringComparison.Ordinal))
        {
            throw new ArgumentException("Le courriel doit contenir un arobase.", nameof(valeur));
        }

        Valeur = normalisee;
    }
}
