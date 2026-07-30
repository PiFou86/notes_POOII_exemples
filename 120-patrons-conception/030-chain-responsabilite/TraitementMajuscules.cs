public sealed class TraitementMajuscules : ITraitementChaineCaracteres
{
    public ITraitementChaineCaracteres? Suivant { get; set; }

    public string Traiter(string chaine)
    {
        ArgumentNullException.ThrowIfNull(chaine);
        string resultat = chaine.ToUpperInvariant();
        return Suivant?.Traiter(resultat) ?? resultat;
    }
}
