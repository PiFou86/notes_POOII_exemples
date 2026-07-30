public sealed class Etudiant
{
    public CodePermanent CodePermanent { get; private set; }
    public string Nom { get; private set; }
    public Courriel Courriel { get; private set; }

    public Etudiant(
        CodePermanent codePermanent,
        string nom,
        Courriel courriel)
    {
        ArgumentNullException.ThrowIfNull(codePermanent);
        ArgumentException.ThrowIfNullOrWhiteSpace(nom);
        ArgumentNullException.ThrowIfNull(courriel);
        CodePermanent = codePermanent;
        Nom = nom.Trim();
        Courriel = courriel;
    }
}
