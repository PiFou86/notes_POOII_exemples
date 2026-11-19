using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services
            .AddOptions<DepotOptions>()
            .Bind(builder.Configuration.GetSection("depot"))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.Format),
                "Le format du dépôt est obligatoire.")
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.Chemin),
                "Le chemin du dépôt est obligatoire.")
            .ValidateOnStart();

        builder.Services.AddTransient<ServiceDepotOptions>();

        using IHost host = builder.Build();

        ServiceDepotOptions serviceOptions = host.Services
            .GetRequiredService<ServiceDepotOptions>();

        if (serviceOptions.ObtenirCheminDepot() != "produits.json")
        {
            throw new InvalidOperationException(
                "La liaison de la configuration a échoué.");
        }

        foreach (string format in new[] { "json", "yaml" })
        {
            string chemin = Path.Combine(
                Path.GetTempPath(),
                $"produits-{Guid.NewGuid():N}.{format}");

            try
            {
                IDepotProduits depot = format == "json"
                    ? new DepotProduitsJson(chemin)
                    : new DepotProduitsYaml(chemin);

                depot.Ajout(new Produit(1, "Clavier", "Informatique"));
                depot.MAJ(new Produit(1, "Clavier ergonomique", "Informatique"));

                if (depot.Obtenir(1)?.Nom != "Clavier ergonomique"
                    || depot.RechercherParMotCle("informatique").Count != 1)
                {
                    throw new InvalidOperationException(
                        $"Dépôt {format} invalide.");
                }

                depot.Supprimer(1);

                if (depot.Obtenir(1) is not null)
                {
                    throw new InvalidOperationException("La suppression a échoué.");
                }
            }
            finally
            {
                File.Delete(chemin);
            }
        }

        Console.Out.WriteLine("Validation réussie.");
    }
}
