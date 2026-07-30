public interface IDepotProduits
{
    Produit? Obtenir(int id);
    void Ajout(Produit produit);
    void MAJ(Produit produit);
    void Supprimer(int id);
    List<Produit> RechercherParMotCle(string motCle);
}
