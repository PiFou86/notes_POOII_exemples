namespace ExempleReservation.Domaine;

public sealed class RegleRabaisAine : IRegleAdmissibiliteRabais
{
    public int AgeMinimum { get; }
    public RabaisApplicable Rabais { get; }

    public RegleRabaisAine(int ageMinimum, RabaisApplicable rabais)
    {
        if (ageMinimum < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ageMinimum));
        }

        ArgumentNullException.ThrowIfNull(rabais);
        AgeMinimum = ageMinimum;
        Rabais = rabais;
    }

    public bool EstAdmissible(ContexteTarificationParticipant contexte)
    {
        ArgumentNullException.ThrowIfNull(contexte);
        return contexte.AgeALaDate >= AgeMinimum;
    }
}
