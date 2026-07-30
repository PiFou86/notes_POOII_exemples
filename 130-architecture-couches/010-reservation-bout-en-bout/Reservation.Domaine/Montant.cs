namespace ExempleReservation.Domaine;

public sealed record Montant
{
    public decimal Valeur { get; }

    public Montant(decimal valeur)
    {
        if (valeur < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(valeur));
        }

        Valeur = decimal.Round(valeur, 2, MidpointRounding.AwayFromZero);
    }

    public Montant Appliquer(PourcentageRabais pourcentage)
    {
        ArgumentNullException.ThrowIfNull(pourcentage);
        return new Montant(Valeur * (1m - pourcentage.Valeur));
    }

    public static Montant operator +(Montant gauche, Montant droite)
    {
        ArgumentNullException.ThrowIfNull(gauche);
        ArgumentNullException.ThrowIfNull(droite);
        return new Montant(gauche.Valeur + droite.Valeur);
    }
}
