namespace ExempleReservation.Domaine;

public sealed class PolitiqueReservation
{
    private readonly List<IRegleAdmissibiliteRabais> m_reglesRabais;

    public int NombreParticipantsMaximum { get; }
    public Montant TarifRegulierParPersonne { get; }

    public PolitiqueReservation(
        int nombreParticipantsMaximum,
        Montant tarifRegulierParPersonne,
        IEnumerable<IRegleAdmissibiliteRabais> reglesRabais)
    {
        if (nombreParticipantsMaximum < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(nombreParticipantsMaximum));
        }

        ArgumentNullException.ThrowIfNull(tarifRegulierParPersonne);

        if (tarifRegulierParPersonne.Valeur <= 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(tarifRegulierParPersonne));
        }

        ArgumentNullException.ThrowIfNull(reglesRabais);
        m_reglesRabais = reglesRabais.ToList();

        if (m_reglesRabais.Any(regle => regle is null))
        {
            throw new ArgumentException(
                "Une règle de rabais ne peut pas être nulle.",
                nameof(reglesRabais));
        }

        VerifierCodesUniques(m_reglesRabais);

        NombreParticipantsMaximum = nombreParticipantsMaximum;
        TarifRegulierParPersonne = tarifRegulierParPersonne;
    }

    public TarificationParticipant Tarifer(
        Participant participant,
        DateOnly dateReservation)
    {
        ArgumentNullException.ThrowIfNull(participant);

        ContexteTarificationParticipant contexte = new(
            participant,
            dateReservation);
        List<RabaisApplicable> admissibles = m_reglesRabais
            .Where(regle => regle.EstAdmissible(contexte))
            .Select(regle => regle.Rabais)
            .ToList();

        RabaisApplicable retenu = admissibles
            .OrderByDescending(rabais => rabais.Pourcentage.Valeur)
            .ThenBy(rabais => rabais.Nature == NatureRabais.Public ? 0 : 1)
            .ThenBy(rabais => rabais.Code, StringComparer.Ordinal)
            .FirstOrDefault()
            ?? new RabaisApplicable(
                "AUCUN",
                NatureRabais.Public,
                new PourcentageRabais(0m));

        return new TarificationParticipant(
            participant.Id,
            contexte.AgeALaDate,
            admissibles.Select(rabais => rabais.Code).Order().ToList(),
            retenu,
            TarifRegulierParPersonne,
            TarifRegulierParPersonne.Appliquer(retenu.Pourcentage));
    }

    private static void VerifierCodesUniques(
        IEnumerable<IRegleAdmissibiliteRabais> reglesRabais)
    {
        bool contientDoublon = reglesRabais
            .GroupBy(regle => regle.Rabais.Code)
            .Any(groupe => groupe.Count() > 1);

        if (contientDoublon)
        {
            throw new ArgumentException(
                "Les codes de rabais doivent être uniques.",
                nameof(reglesRabais));
        }
    }
}
