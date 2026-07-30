using System.Reflection;

public static class InspecteurReflection
{
    public static IReadOnlyList<string> ObtenirProprietes(Type type) => type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(propriete => propriete.Name).ToList();
    public static Produit CreerProduit() => (Produit)(Activator.CreateInstance(typeof(Produit)) ?? throw new InvalidOperationException());
}
