public static class Program
{
    public static void Main(string[] args)
    {
        Catalogue catalogue = new();
        catalogue.Ajouter("Café");
        if (catalogue.ObtenirTous().Count != 1) { throw new InvalidOperationException("CQS invalide."); }
        IForme[] formes = [new Rectangle(3, 4), new Carre(3)];
        if (formes.Sum(forme => forme.CalculerAire()) != 21) { throw new InvalidOperationException("Abstraction IForme invalide."); }
        MetriquesDependances metriques = new(2, 1);
        if (metriques.Instabilite is not double instabilite
            || Math.Round(instabilite, 2) != 0.33)
        {
            throw new InvalidOperationException("I devrait valoir 0,33.");
        }

        Console.Out.WriteLine(
            $"Ca={metriques.Ca}, Ce={metriques.Ce}, I={instabilite:F2}");
    }
}
