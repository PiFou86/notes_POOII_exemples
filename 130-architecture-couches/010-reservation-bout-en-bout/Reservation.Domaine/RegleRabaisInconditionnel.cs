namespace ExempleReservation.Domaine;

public sealed class RegleRabaisInconditionnel : IRegleAdmissibiliteRabais
{
    public RabaisApplicable Rabais { get; }

    public RegleRabaisInconditionnel(RabaisApplicable rabais)
    {
        ArgumentNullException.ThrowIfNull(rabais);
        Rabais = rabais;
    }

    public bool EstAdmissible(ContexteTarificationParticipant contexte)
    {
        ArgumentNullException.ThrowIfNull(contexte);
        return true;
    }
}
