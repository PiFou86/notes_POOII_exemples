public sealed class DepotProduitsMemoire : IDepotProduits
{
    private readonly List<Produit> m_produits = [];

    public Produit? Obtenir(int id)
    {
        return m_produits.SingleOrDefault(produit => produit.Id == id);
    }

    public void Ajout(Produit produit)
    {
        ArgumentNullException.ThrowIfNull(produit);

        if (Obtenir(produit.Id) is not null)
        {
            throw new InvalidOperationException(
                "Un produit possède déjà cet identifiant.");
        }

        m_produits.Add(produit);
    }

    public void MAJ(Produit produit)
    {
        ArgumentNullException.ThrowIfNull(produit);
        int index = m_produits.FindIndex(
            element => element.Id == produit.Id);

        if (index < 0)
        {
            throw new InvalidOperationException("Produit introuvable.");
        }

        m_produits[index] = produit;
    }

    public void Supprimer(int id)
    {
        m_produits.RemoveAll(produit => produit.Id == id);
    }

    public List<Produit> RechercherParMotCle(string motCle)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(motCle);
        return m_produits
            .Where(produit => produit.Nom.Contains(
                motCle,
                StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
