public static class Program
{
    public static void Main(string[] args)
    {
        int valeur = 10;
        int copieValeur = valeur;
        copieValeur++;
        if (valeur != 10)
        {
            throw new InvalidOperationException("La copie d'une valeur ne devrait pas modifier l'original.");
        }

        Cours cours = new Cours(" 420-W30-SF ", " Programmation orientée objet II ");
        Cours copieReference = cours;
        if (!ReferenceEquals(cours, copieReference))
        {
            throw new InvalidOperationException("Les variables devraient référencer le même objet.");
        }

        Cours coursEquivalent = new Cours("420-w30-sf", "Programmation orientée objet II");
        if (!cours.Equals(coursEquivalent) || cours.GetHashCode() != coursEquivalent.GetHashCode())
        {
            throw new InvalidOperationException("Le contrat d'égalité est incohérent.");
        }

        Calculatrice calculatrice = new Calculatrice();
        if (calculatrice.Additionner(2, 3) != 5
            || calculatrice.Additionner(2.5f, 3.5f) != 6f
            || calculatrice.Additionner(2, 3.5f) != 5.5f
            || calculatrice.Additionner(1, 2, 3) != 6)
        {
            throw new InvalidOperationException("Une surcharge a produit un mauvais résultat.");
        }

        Console.Out.WriteLine(cours);
        Console.Out.WriteLine("Validation réussie.");
    }
}
