public static class Program
{
    public static async Task Main(string[] args)
    {
        Produit produit = InspecteurReflection.CreerProduit(); produit.Nom = "Café";
        if (!InspecteurReflection.ObtenirProprietes(typeof(Produit)).Contains(nameof(Produit.Nom))) { throw new InvalidOperationException("Réflexion invalide."); }
        ServiceAsynchrone service = new();
        await service.AttendreAsync(CancellationToken.None);
        if (await service.CalculerAsync() != 42 || await service.ObtenirNomAsync(1) != "Alice") { throw new InvalidOperationException("Asynchronisme invalide."); }
        Console.Out.WriteLine("Validation réussie.");
    }
}
