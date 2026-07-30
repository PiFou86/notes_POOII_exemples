namespace ExempleReservation.Domaine;

public sealed record PourcentageRabais
{
    public decimal Valeur { get; }

    public PourcentageRabais(decimal valeur)
    {
        if (valeur is < 0m or > 1m)
        {
            throw new ArgumentOutOfRangeException(nameof(valeur));
        }

        Valeur = valeur;
    }
}
