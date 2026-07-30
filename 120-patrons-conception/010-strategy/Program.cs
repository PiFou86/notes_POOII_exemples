Facture facture = new(100m, new RabaisEtudiant());
if (facture.CalculerTotal() != 85m)
{
    throw new InvalidOperationException("Le total devrait être de 85,00.");
}

Console.Out.WriteLine("Validation réussie.");
