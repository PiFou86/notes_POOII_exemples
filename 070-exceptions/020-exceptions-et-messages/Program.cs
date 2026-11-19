using System.Text.Json;

public static class Program
{
    public static void Main(string[] args)
    {
        Commande commande = new Commande();
        commande.Facturer();

        bool etatInvalideRefuse = false;
        try
        {
            commande.AjouterLigne();
        }
        catch (CommandeDansEtatInvalideException exception)
        {
            etatInvalideRefuse = true;
            Console.Error.WriteLine(exception.Message);
        }

        if (!etatInvalideRefuse)
        {
            throw new InvalidOperationException("L'état invalide aurait dû être refusé.");
        }

        JournalMemoire journal = new JournalMemoire();
        string messageUtilisateur;
        try
        {
            ChargerCatalogue("{");
            throw new InvalidOperationException("Le document JSON invalide aurait dû être refusé.");
        }
        catch (ImportCatalogueException exception)
        {
            journal.Erreur(exception);
            messageUtilisateur = "Les données ne peuvent pas être chargées pour le moment.";
        }

        if (journal.DerniereException is not ImportCatalogueException importException
            || importException.InnerException is not JsonException
            || messageUtilisateur.Contains("/chemin", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Le traitement sécuritaire est invalide.");
        }

        Console.Out.WriteLine(messageUtilisateur);
        Console.Out.WriteLine("Validation réussie.");

        static void ChargerCatalogue(string contenuJson)
        {
            try
            {
                using JsonDocument document = JsonDocument.Parse(contenuJson);
            }
            catch (JsonException exception)
            {
                throw new ImportCatalogueException(
                    "Le catalogue ne contient pas un document JSON valide.",
                    exception);
            }
        }
    }
}
