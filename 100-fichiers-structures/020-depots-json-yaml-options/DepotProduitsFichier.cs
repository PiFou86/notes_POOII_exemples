public abstract class DepotProduitsFichier : IDepotProduits
{
    protected string Chemin { get; }

    protected DepotProduitsFichier(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);

        Chemin = chemin;
    }

    protected abstract List<ProduitPersistanceDto> LireDtos();
    protected abstract void EcrireDtos(List<ProduitPersistanceDto> produits);

    public Produit? Obtenir(int id)
    {
        return LireTous().SingleOrDefault(p => p.Id == id);
    }

    public void Ajout(Produit produit)
    {
        ArgumentNullException.ThrowIfNull(produit);

        List<Produit> produits = LireTous();

        if (produits.Any(p => p.Id == produit.Id))
        {
            throw new InvalidOperationException("Identifiant dupliqué.");
        }

        produits.Add(produit);
        EcrireTous(produits);
    }

    public void MAJ(Produit produit)
    {
        ArgumentNullException.ThrowIfNull(produit);

        List<Produit> produits = LireTous();
        int index = produits.FindIndex(p => p.Id == produit.Id);

        if (index < 0)
        {
            throw new KeyNotFoundException();
        }

        produits[index] = produit;
        EcrireTous(produits);
    }

    public void Supprimer(int id)
    {
        List<Produit> produits = LireTous();
        produits.RemoveAll(p => p.Id == id);
        EcrireTous(produits);
    }

    public List<Produit> RechercherParMotCle(string motCle)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(motCle);

        return LireTous()
            .Where(p =>
                p.Nom.Contains(motCle, StringComparison.OrdinalIgnoreCase)
                || p.Categorie.Contains(
                    motCle,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private List<Produit> LireTous()
    {
        return LireDtos()
            .Select(dto => dto.VersEntite())
            .ToList();
    }

    private void EcrireTous(List<Produit> produits)
    {
        List<ProduitPersistanceDto> dtos = produits
            .Select(ProduitPersistanceDto.Depuis)
            .ToList();

        EcrireDtos(dtos);
    }
}
