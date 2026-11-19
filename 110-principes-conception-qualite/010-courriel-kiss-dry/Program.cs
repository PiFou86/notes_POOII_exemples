public static class Program
{
    public static void Main(string[] args)
    {
        Courriel courriel = new("  alice@college.ca  ");
        if (courriel.Valeur != "alice@college.ca")
        {
            throw new InvalidOperationException("Le courriel n'a pas été normalisé.");
        }

        Courriel memeCourriel = new("ALICE@COLLEGE.CA");
        if (!courriel.Equals(memeCourriel)
            || courriel.GetHashCode() != memeCourriel.GetHashCode())
        {
            throw new InvalidOperationException("L'égalité par valeur est invalide.");
        }

        foreach (string valeur in new[]
        {
            "alicecollege.ca",
            "alice@@college.ca",
            "@college.ca",
            "alice@"
        })
        {
            try
            {
                _ = new Courriel(valeur);
                throw new InvalidOperationException($"Le courriel {valeur} aurait dû être refusé.");
            }
            catch (ArgumentException)
            {
            }
        }

        CalculateurTotal calculateur = new();
        if (calculateur.CalculerTotalAvecTaxes(100m) != 114.975m)
        {
            throw new InvalidOperationException("Le total est incorrect.");
        }

        Console.Out.WriteLine("Validation réussie.");
    }
}
