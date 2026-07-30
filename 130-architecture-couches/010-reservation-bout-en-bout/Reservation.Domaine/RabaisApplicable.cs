namespace ExempleReservation.Domaine;

public enum NatureRabais
{
    Public,
    Promotion
}

public sealed record RabaisApplicable
{
    public string Code { get; }
    public NatureRabais Nature { get; }
    public PourcentageRabais Pourcentage { get; }

    public RabaisApplicable(
        string code,
        NatureRabais nature,
        PourcentageRabais pourcentage)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentNullException.ThrowIfNull(pourcentage);

        if (!Enum.IsDefined(nature))
        {
            throw new ArgumentOutOfRangeException(nameof(nature));
        }

        Code = code.Trim().ToUpperInvariant();
        Nature = nature;
        Pourcentage = pourcentage;
    }
}
