namespace ExempleReservation.Domaine;

public enum EtatReservation
{
    Brouillon,
    Confirmee
}

public sealed class Reservation
{
    private readonly List<Participant> m_participants = new();
    private readonly List<TarificationParticipant> m_tarifications = new();

    public Guid Id { get; }
    public string NomClient { get; }
    public DateTimeOffset DatePrevue { get; }
    public EtatReservation Etat { get; private set; }
    public IReadOnlyList<Participant> Participants => m_participants.AsReadOnly();
    public IReadOnlyList<TarificationParticipant> Tarifications =>
        m_tarifications.AsReadOnly();
    public Montant? Total { get; private set; }

    private Reservation(Guid id, string nomClient, DateTimeOffset datePrevue)
    {
        Id = id;
        NomClient = nomClient.Trim();
        DatePrevue = datePrevue;
        Etat = EtatReservation.Brouillon;
    }

    public static ResultatCreationReservation CreerBrouillon(
        Guid id,
        string nomClient,
        DateTimeOffset datePrevue,
        DateTimeOffset maintenant)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(
                "L'identifiant est obligatoire.",
                nameof(id));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(nomClient);

        if (datePrevue <= maintenant)
        {
            return ResultatCreationReservation.Refuser(
                CodeRefusReservation.DateNonFuture);
        }

        Reservation reservation = new(id, nomClient, datePrevue);
        return ResultatCreationReservation.Accepter(reservation);
    }

    public DecisionReservation AjouterParticipant(
        Participant participant,
        PolitiqueReservation politique)
    {
        ArgumentNullException.ThrowIfNull(participant);
        ArgumentNullException.ThrowIfNull(politique);

        if (Etat != EtatReservation.Brouillon)
        {
            return DecisionReservation.Refuser(
                CodeRefusReservation.ReservationConfirmee);
        }

        if (participant.DateNaissance > DateOnly.FromDateTime(DatePrevue.Date))
        {
            return DecisionReservation.Refuser(
                CodeRefusReservation.DateNaissanceApresReservation);
        }

        bool existeDeja = m_participants.Any(p =>
            p.Nom.Equals(participant.Nom, StringComparison.OrdinalIgnoreCase)
            && p.DateNaissance == participant.DateNaissance);

        if (existeDeja)
        {
            return DecisionReservation.Refuser(
                CodeRefusReservation.ParticipantDuplique);
        }

        if (m_participants.Count >= politique.NombreParticipantsMaximum)
        {
            return DecisionReservation.Refuser(
                CodeRefusReservation.CapaciteAtteinte);
        }

        m_participants.Add(participant);
        return DecisionReservation.Accepter();
    }

    public DecisionReservation RetirerParticipant(Guid participantId)
    {
        if (participantId == Guid.Empty)
        {
            throw new ArgumentException(
                "L'identifiant du participant est obligatoire.",
                nameof(participantId));
        }

        if (Etat != EtatReservation.Brouillon)
        {
            return DecisionReservation.Refuser(
                CodeRefusReservation.ReservationConfirmee);
        }

        Participant? participant = m_participants.SingleOrDefault(
            p => p.Id == participantId);

        if (participant is null)
        {
            return DecisionReservation.Refuser(
                CodeRefusReservation.ParticipantIntrouvable);
        }

        m_participants.Remove(participant);
        return DecisionReservation.Accepter();
    }

    public DecisionReservation Confirmer(PolitiqueReservation politique)
    {
        ArgumentNullException.ThrowIfNull(politique);

        if (Etat != EtatReservation.Brouillon)
        {
            return DecisionReservation.Refuser(
                CodeRefusReservation.ReservationConfirmee);
        }

        if (m_participants.Count == 0)
        {
            return DecisionReservation.Refuser(
                CodeRefusReservation.ReservationVide);
        }

        DateOnly dateReservation = DateOnly.FromDateTime(DatePrevue.Date);
        m_tarifications.Clear();
        m_tarifications.AddRange(m_participants.Select(
            participant => politique.Tarifer(participant, dateReservation)));

        Montant total = new(0m);
        foreach (TarificationParticipant tarification in m_tarifications)
        {
            total += tarification.TarifFinal;
        }

        Total = total;
        Etat = EtatReservation.Confirmee;
        return DecisionReservation.Accepter();
    }
}
