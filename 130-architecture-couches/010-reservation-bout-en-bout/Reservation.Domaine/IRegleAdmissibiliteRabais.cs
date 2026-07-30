namespace ExempleReservation.Domaine;

public interface IRegleAdmissibiliteRabais
{
    RabaisApplicable Rabais { get; }
    bool EstAdmissible(ContexteTarificationParticipant contexte);
}
