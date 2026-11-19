using BibliothequeSalutations;

public static class Program
{
    public static void Main(string[] args)
    {
        GenerateurSalutation generateur = new GenerateurSalutation();
        string message = generateur.Creer("Alice");
        if (message != "Bonjour Alice !")
        {
            throw new InvalidOperationException("La référence de projet est invalide.");
        }

        Console.Out.WriteLine(message);
    }
}
