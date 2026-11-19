using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Logging.ClearProviders();
        string? langue = builder.Configuration["langue"];
        string? format = builder.Configuration["depot:format"];

        switch (langue)
        {
            case "fr":
                builder.Services.AddScoped<ISalutation, SalutationFrancais>();
                break;
            case "en":
                builder.Services.AddScoped<ISalutation, SalutationAnglais>();
                break;
            case "eu":
                builder.Services.AddScoped<ISalutation, SalutationBasque>();
                break;
            default:
                throw new ArgumentException("Langue non supportée.", nameof(langue));
        }

        switch (format)
        {
            case "json":
                builder.Services.AddScoped<IDepotProduits, DepotProduitsJson>();
                break;
            case "xml":
                builder.Services.AddScoped<IDepotProduits, DepotProduitsXml>();
                break;
            case "yaml":
                builder.Services.AddScoped<IDepotProduits, DepotProduitsYaml>();
                break;
            default:
                throw new ArgumentException("Format non supporté.", nameof(format));
        }

        builder.Services.Configure<DepotOptions>(
            builder.Configuration.GetSection("depot"));
        builder.Services.AddScoped<ServiceAccueil>();
        builder.Services.AddScoped<InformationDepot>();
        builder.Services.AddScoped<RessourcePortee>();
        using IHost host = builder.Build();

        ISalutation premierePortee;
        RessourcePortee ressourcePortee;
        using (IServiceScope scope = host.Services.CreateScope())
        {
            premierePortee = scope.ServiceProvider.GetRequiredService<ISalutation>();
            ISalutation memePortee = scope.ServiceProvider.GetRequiredService<ISalutation>();
            if (!ReferenceEquals(premierePortee, memePortee))
            {
                throw new InvalidOperationException("Un service Scoped devrait être partagé dans sa portée.");
            }

            ServiceAccueil serviceAccueil =
                scope.ServiceProvider.GetRequiredService<ServiceAccueil>();
            IDepotProduits depot = scope.ServiceProvider.GetRequiredService<IDepotProduits>();
            InformationDepot informationDepot =
                scope.ServiceProvider.GetRequiredService<InformationDepot>();
            ressourcePortee = scope.ServiceProvider.GetRequiredService<RessourcePortee>();
            Console.Out.WriteLine(serviceAccueil.Saluer("Alice"));
            Console.Out.WriteLine($"Dépôt {depot.Format}.");
            Console.Out.WriteLine(informationDepot.ObtenirDescription());
        }

        if (!ressourcePortee.EstLiberee)
        {
            throw new InvalidOperationException(
                "Le conteneur devrait libérer les services à la fin de la portée.");
        }

        using (IServiceScope scope = host.Services.CreateScope())
        {
            ISalutation nouvellePortee = scope.ServiceProvider.GetRequiredService<ISalutation>();
            if (ReferenceEquals(premierePortee, nouvellePortee))
            {
                throw new InvalidOperationException("Une nouvelle portée devrait recevoir une nouvelle instance.");
            }
        }

        Console.Out.WriteLine("Validation réussie.");
    }
}
