IDepotProduits depot = new DepotProduitsMemoire();
depot.Ajout(new Produit(1, "Café"));

ControleurProduits controleur = new(depot, new VueProduit());
if (controleur.Afficher(1) != "1: Café")
{
    throw new InvalidOperationException("MVC invalide.");
}

Rapport rapport = new RapportBuilder()
    .AvecTitre("Ventes")
    .AjouterSection("Résumé")
    .Construire();

CreateurExporteur createur = new CreateurExporteurJson();
string export = createur.Exporter(rapport.Titre);
if (!export.Contains("Ventes", StringComparison.Ordinal))
{
    throw new InvalidOperationException("Patrons invalides.");
}

Console.Out.WriteLine("Validation réussie.");
