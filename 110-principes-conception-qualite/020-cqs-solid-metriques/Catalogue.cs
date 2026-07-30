public sealed class Catalogue
{
    private readonly List<string> m_noms = [];
    public void Ajouter(string nom) { ArgumentException.ThrowIfNullOrWhiteSpace(nom); m_noms.Add(nom); }
    public IReadOnlyList<string> ObtenirTous() => m_noms.AsReadOnly();
}
