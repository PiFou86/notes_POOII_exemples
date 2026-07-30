namespace ExempleReservation.Domaine;

public sealed record TarificationParticipant
{
    public Guid ParticipantId { get; }
    public int Age { get; }
    public IReadOnlyList<string> CodesAdmissibles { get; }
    public RabaisApplicable RabaisRetenu { get; }
    public Montant TarifRegulier { get; }
    public Montant TarifFinal { get; }

    public TarificationParticipant(
        Guid participantId,
        int age,
        IReadOnlyList<string> codesAdmissibles,
        RabaisApplicable rabaisRetenu,
        Montant tarifRegulier,
        Montant tarifFinal)
    {
        if (participantId == Guid.Empty)
        {
            throw new ArgumentException(
                "L'identifiant du participant est obligatoire.",
                nameof(participantId));
        }

        if (age < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(age));
        }

        ArgumentNullException.ThrowIfNull(codesAdmissibles);
        ArgumentNullException.ThrowIfNull(rabaisRetenu);
        ArgumentNullException.ThrowIfNull(tarifRegulier);
        ArgumentNullException.ThrowIfNull(tarifFinal);

        if (codesAdmissibles.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException(
                "Un code de rabais admissible ne peut pas être vide.",
                nameof(codesAdmissibles));
        }

        if (tarifFinal.Valeur > tarifRegulier.Valeur)
        {
            throw new ArgumentOutOfRangeException(
                nameof(tarifFinal),
                "Le tarif final ne peut pas dépasser le tarif régulier.");
        }

        ParticipantId = participantId;
        Age = age;
        CodesAdmissibles = codesAdmissibles.ToList().AsReadOnly();
        RabaisRetenu = rabaisRetenu;
        TarifRegulier = tarifRegulier;
        TarifFinal = tarifFinal;
    }
}
