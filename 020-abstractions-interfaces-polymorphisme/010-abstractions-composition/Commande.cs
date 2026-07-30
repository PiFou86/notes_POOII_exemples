public sealed class Commande
{
    private readonly List<LigneCommande> m_lignes = new List<LigneCommande>();

    public IReadOnlyList<LigneCommande> Lignes
    {
        get
        {
            return m_lignes.AsReadOnly();
        }
    }

    public void AjouterLigne(LigneCommande ligne)
    {
        ArgumentNullException.ThrowIfNull(ligne);
        m_lignes.Add(ligne);
    }
}
