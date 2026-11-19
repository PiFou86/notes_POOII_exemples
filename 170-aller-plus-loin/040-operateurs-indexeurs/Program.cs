public static class Program
{
    public static void Main(string[] args)
    {
        MesureMetre longueurA = new MesureMetre(2.5);
        MesureMetre longueurB = new MesureMetre(1.25);
        MesureMetre total = longueurA + longueurB;
        if (total.Valeur != 3.75)
        {
            throw new InvalidOperationException("L'addition de deux mesures est incorrecte.");
        }

        MesureMetre difference = new MesureMetre(2) - new MesureMetre(2);
        if (difference.Valeur != 0)
        {
            throw new InvalidOperationException("Une différence nulle devrait être valide.");
        }

        bool differenceNegativeRefusee = false;
        try
        {
            MesureMetre invalide = new MesureMetre(2) - new MesureMetre(3);
        }
        catch (ArgumentOutOfRangeException)
        {
            differenceNegativeRefusee = true;
        }

        if (!differenceNegativeRefusee)
        {
            throw new InvalidOperationException("Une différence négative aurait dû être refusée.");
        }

        Matrice matrice = new Matrice(2, 3);
        matrice[1, 2] = 7.5;
        if (matrice[1, 2] != 7.5)
        {
            throw new InvalidOperationException("L'indexeur devrait retourner la valeur assignée.");
        }

        bool indexInvalideRefuse = false;
        try
        {
            double valeurInvalide = matrice[0, 3];
        }
        catch (ArgumentOutOfRangeException)
        {
            indexInvalideRefuse = true;
        }

        if (!indexInvalideRefuse)
        {
            throw new InvalidOperationException("L'indexeur devrait valider les index.");
        }

        Console.Out.WriteLine(total);
        Console.Out.WriteLine("Validation réussie.");
    }
}
