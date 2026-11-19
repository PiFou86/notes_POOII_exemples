public static class Program
{
    public static async Task Main(string[] args)
    {
        DepotMemoire<EtudiantIdentifiable> depot = new();
        depot.Ajouter(new EtudiantIdentifiable(1, "Alice"));
        if (depot.TrouverParId(1)?.Nom != "Alice")
        {
            throw new InvalidOperationException("Le dépôt devrait retrouver Alice.");
        }

        string chemin = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(chemin, ["première", "deuxième"]);
            List<string> lignes = [];
            await foreach (string ligne in LectureProgressive.LireLignesAsync(chemin))
            {
                lignes.Add(ligne);
            }

            if (lignes.Count != 2)
            {
                throw new InvalidOperationException("Deux lignes étaient attendues.");
            }
        }
        finally
        {
            File.Delete(chemin);
        }

        Console.Out.WriteLine("Validation réussie.");
    }
}
