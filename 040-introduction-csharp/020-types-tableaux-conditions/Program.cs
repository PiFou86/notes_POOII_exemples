public static class Program
{
    public static void Main(string[] args)
    {
        int age = 20;
        double moyenne = 78.5;
        string nom = "Alice";
        string message = $"{nom} a {age} ans et une moyenne de {moyenne}.";
        if (!message.Contains("Alice") || !message.Contains("20"))
        {
            throw new InvalidOperationException("La chaîne interpolée est incorrecte.");
        }

        string chemin = @"C:\cours\notes.txt";
        if (!chemin.Contains('\\'))
        {
            throw new InvalidOperationException("La chaîne verbatim devrait conserver les barres obliques inverses.");
        }

        string[] noms = new string[] { "Alice", "Bob", "Chloé", "David" };
        string dernier = noms[^1];
        string[] milieu = noms[1..3];
        if (noms.Length != 4 || dernier != "David" || milieu.Length != 2 || milieu[0] != "Bob")
        {
            throw new InvalidOperationException("Les index ou plages sont incorrects.");
        }

        int[,] grille = new int[2, 3];
        grille[1, 2] = 7;
        if (grille.GetLength(0) != 2 || grille.GetLength(1) != 3 || grille[1, 2] != 7)
        {
            throw new InvalidOperationException("Le tableau à deux dimensions est incorrect.");
        }

        int total = 0;
        foreach (string prenom in noms)
        {
            if (prenom.Length > 3)
            {
                total++;
            }
        }

        if (total != 3)
        {
            throw new InvalidOperationException("La boucle devrait compter trois prénoms longs.");
        }

        string? saisie = null;
        string nomAffiche = saisie ?? "Anonyme";
        string categorie = age >= 18 ? "adulte" : "mineur";
        string? preference = null;
        preference ??= "aucune";
        if (nomAffiche != "Anonyme" || categorie != "adulte" || preference != "aucune")
        {
            throw new InvalidOperationException("Les expressions conditionnelles sont incorrectes.");
        }

        Console.Out.WriteLine(message);
        Console.Out.WriteLine("Validation réussie.");
    }
}
