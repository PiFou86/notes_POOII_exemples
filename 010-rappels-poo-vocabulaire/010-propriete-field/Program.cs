public static class Program
{
    public static void Main(string[] args)
    {
        Produit produit = new Produit();
        produit.Nom = " Clavier ";

        if (produit.Nom != "Clavier")
        {
            throw new InvalidOperationException("Le nom devrait être normalisé.");
        }

        bool nomVideRefuse = false;
        try
        {
            produit.Nom = " ";
        }
        catch (ArgumentException)
        {
            nomVideRefuse = true;
        }

        if (!nomVideRefuse)
        {
            throw new InvalidOperationException("Un nom vide aurait dû être refusé.");
        }

        Console.Out.WriteLine("Validation réussie.");
    }
}
