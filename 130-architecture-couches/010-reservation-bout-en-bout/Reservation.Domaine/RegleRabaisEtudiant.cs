namespace ExempleReservation.Domaine;

public sealed class RegleRabaisEtudiant : IRegleAdmissibiliteRabais
{
    public RabaisApplicable Rabais { get; }

    public RegleRabaisEtudiant(RabaisApplicable rabais)
    {
        ArgumentNullException.ThrowIfNull(rabais);
        Rabais = rabais;
    }

    public bool EstAdmissible(ContexteTarificationParticipant contexte)
    {
        ArgumentNullException.ThrowIfNull(contexte);
        return contexte.Participant.EstEtudiant;
    }
}
