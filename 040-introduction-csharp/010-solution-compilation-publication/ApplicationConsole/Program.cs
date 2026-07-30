using BibliothequeSalutations;

GenerateurSalutation generateur = new GenerateurSalutation();
string message = generateur.Creer("Alice");
if (message != "Bonjour Alice !")
{
    throw new InvalidOperationException("La référence de projet est invalide.");
}

Console.Out.WriteLine(message);
