public static class Program
{
    public static void Main(string[] args)
    {
        Forme forme = new Rectangle { Largeur = 3, Hauteur = 4, Couleur = "bleu" };
        if (forme.CalculerAire() != 12)
        {
            throw new InvalidOperationException("Le polymorphisme devrait calculer une aire de 12.");
        }

        IExportable exportable = new Rapport();
        exportable.Exporter();

        Commande commande = new Commande();
        commande.AjouterLigne(new LigneCommande("Café", 3.50m));
        if (commande.Lignes.Count != 1)
        {
            throw new InvalidOperationException("La commande devrait contenir une ligne.");
        }

        Console.Out.WriteLine("Validation réussie.");
    }
}
