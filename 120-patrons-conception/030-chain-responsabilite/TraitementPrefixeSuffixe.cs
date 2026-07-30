public sealed class TraitementPrefixeSuffixe : ITraitementChaineCaracteres
{
    private readonly string m_prefixe;
    private readonly string m_suffixe;

    public ITraitementChaineCaracteres? Suivant { get; set; }

    public TraitementPrefixeSuffixe(string prefixe, string suffixe)
    {
        ArgumentNullException.ThrowIfNull(prefixe);
        ArgumentNullException.ThrowIfNull(suffixe);
        m_prefixe = prefixe;
        m_suffixe = suffixe;
    }

    public string Traiter(string chaine)
    {
        ArgumentNullException.ThrowIfNull(chaine);
        string resultat = $"{m_prefixe}{chaine}{m_suffixe}";
        return Suivant?.Traiter(resultat) ?? resultat;
    }
}
