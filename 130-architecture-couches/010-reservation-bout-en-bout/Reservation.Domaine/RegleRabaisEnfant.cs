namespace ExempleReservation.Domaine;

public sealed class RegleRabaisEnfant : IRegleAdmissibiliteRabais
{
    public int AgeMaximum { get; }
    public RabaisApplicable Rabais { get; }

    public RegleRabaisEnfant(int ageMaximum, RabaisApplicable rabais)
    {
        if (ageMaximum < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ageMaximum));
        }

        ArgumentNullException.ThrowIfNull(rabais);
        AgeMaximum = ageMaximum;
        Rabais = rabais;
    }

    public bool EstAdmissible(ContexteTarificationParticipant contexte)
    {
        ArgumentNullException.ThrowIfNull(contexte);
        return contexte.AgeALaDate <= AgeMaximum;
    }
}
