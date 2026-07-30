public sealed class VueProduit
{
    public string Presenter(Produit produit)
    {
        ArgumentNullException.ThrowIfNull(produit);
        return $"{produit.Id}: {produit.Nom}";
    }
}
