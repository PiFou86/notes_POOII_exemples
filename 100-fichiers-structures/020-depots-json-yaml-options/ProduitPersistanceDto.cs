public sealed class ProduitPersistanceDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = "";
    public string Categorie { get; set; } = "";

    public Produit VersEntite()
    {
        return new Produit(Id, Nom, Categorie);
    }

    public static ProduitPersistanceDto Depuis(Produit produit)
    {
        return new ProduitPersistanceDto
        {
            Id = produit.Id,
            Nom = produit.Nom,
            Categorie = produit.Categorie
        };
    }
}
