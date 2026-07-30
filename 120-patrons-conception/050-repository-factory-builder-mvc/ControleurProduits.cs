public sealed class ControleurProduits
{
    private readonly IDepotProduits m_depot;
    private readonly VueProduit m_vue;

    public ControleurProduits(IDepotProduits depot, VueProduit vue)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(vue);
        m_depot = depot;
        m_vue = vue;
    }

    public string Afficher(int id)
    {
        Produit? produit = m_depot.Obtenir(id);
        return produit is null ? "Introuvable" : m_vue.Presenter(produit);
    }
}
