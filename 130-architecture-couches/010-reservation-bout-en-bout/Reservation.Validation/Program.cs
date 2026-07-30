using ExempleReservation.Application;
using ExempleReservation.Domaine;
using ExempleReservation.Infrastructure;
using Microsoft.Extensions.Options;

DateTimeOffset maintenant = new(2026, 2, 1, 12, 0, 0, TimeSpan.Zero);
FausseHorloge horloge = new(maintenant);
IDepotReservations depot = new DepotReservationsMemoire();
PolitiqueReservation politique = CreerPolitique(4);
IFournisseurPolitiqueReservation fournisseur =
    new FournisseurPolitiqueFixe(politique);

CreerBrouillonReservation creer = new(depot, horloge);
AjouterParticipantReservation ajouter = new(depot, fournisseur);
RetirerParticipantReservation retirer = new(depot);
ConfirmerReservation confirmer = new(depot, fournisseur);

Result<ReservationDto> datePassee = creer.Executer(
    new CreerBrouillonReservationCommande("Alice", maintenant.AddMinutes(-1)));
Verifier(!datePassee.EstSucces, "Une date passée doit être refusée.");

ReservationDto reservation = ExigerSucces(creer.Executer(
    new CreerBrouillonReservationCommande("Alice", maintenant.AddMonths(1))));
DateOnly date = DateOnly.FromDateTime(reservation.DatePrevue.Date);

reservation = Ajouter("Enfant", date.AddYears(-10), false);
reservation = Ajouter("Étudiante", date.AddYears(-20), true);
reservation = Ajouter("Adulte", date.AddYears(-35), false);
reservation = Ajouter("Aînée", date.AddYears(-70), false);

Result<ReservationDto> capacite = ajouter.Executer(
    new AjouterParticipantReservationCommande(
        reservation.Id,
        "Personne en trop",
        date.AddYears(-40),
        false));
Verifier(!capacite.EstSucces, "La capacité maximale doit être respectée.");

Guid adulteId = reservation.Participants.Single(p => p.Nom == "Adulte").Id;
reservation = ExigerSucces(retirer.Executer(
    new RetirerParticipantReservationCommande(reservation.Id, adulteId)));
reservation = Ajouter("Nouvelle adulte", date.AddYears(-40), false);
reservation = ExigerSucces(confirmer.Executer(
    new ConfirmerReservationCommande(reservation.Id)));

Verifier(reservation.Etat == "Confirmee", "La réservation doit être confirmée.");
Verifier(reservation.Total == 91.50m, "Le total doit être de 91,50 $." );
Verifier(Tarif("Enfant").CodeRabaisRetenu == "ENFANT", "Rabais enfant attendu.");
Verifier(Tarif("Étudiante").CodeRabaisRetenu == "ETUDIANT", "Rabais étudiant attendu.");
Verifier(Tarif("Aînée").CodeRabaisRetenu == "AINE", "Rabais aîné attendu.");
Verifier(Tarif("Nouvelle adulte").CodeRabaisRetenu == "SAINT_VALENTIN", "Promotion attendue.");

Result<ReservationDto> modificationApresConfirmation = ajouter.Executer(
    new AjouterParticipantReservationCommande(
        reservation.Id,
        "Trop tard",
        date.AddYears(-30),
        false));
Verifier(
    !modificationApresConfirmation.EstSucces,
    "Une réservation confirmée doit être non modifiable.");

VerifierConfigurationInvalide();
VerifierReglesRabais(date);
VerifierPolitiqueSansRabaisEtDepartage(date);
VerifierPreconditions(date);
VerifierResultatsCreation(maintenant);
Console.Out.WriteLine("Validations de l'architecture réussies.");
return;

ReservationDto Ajouter(string nom, DateOnly naissance, bool estEtudiant)
{
    return ExigerSucces(ajouter.Executer(
        new AjouterParticipantReservationCommande(
            reservation.Id,
            nom,
            naissance,
            estEtudiant)));
}

TarificationParticipantDto Tarif(string nom)
{
    Guid participantId = reservation.Participants.Single(p => p.Nom == nom).Id;
    return reservation.Tarifications.Single(t => t.ParticipantId == participantId);
}

static PolitiqueReservation CreerPolitique(int maximum)
{
    List<IRegleAdmissibiliteRabais> reglesRabais = new()
    {
        new RegleRabaisEnfant(
            12,
            Rabais("ENFANT", NatureRabais.Public, 0.50m)),
        new RegleRabaisEtudiant(
            Rabais("ETUDIANT", NatureRabais.Public, 0.15m)),
        new RegleRabaisAine(
            65,
            Rabais("AINE", NatureRabais.Public, 0.20m)),
        new RegleRabaisInconditionnel(
            Rabais("SAINT_VALENTIN", NatureRabais.Promotion, 0.10m))
    };

    return new PolitiqueReservation(
        maximum,
        new Montant(30m),
        reglesRabais);
}

static RabaisApplicable Rabais(
    string code,
    NatureRabais nature,
    decimal pourcentage)
{
    return new RabaisApplicable(code, nature, new PourcentageRabais(pourcentage));
}

static T ExigerSucces<T>(Result<T> resultat)
{
    if (!resultat.EstSucces || resultat.Valeur is null)
    {
        throw new InvalidOperationException(resultat.MessageErreur);
    }

    return resultat.Valeur;
}

static void Verifier(bool condition, string message)
{
    if (!condition)
    {
        throw new InvalidOperationException(message);
    }
}

static void VerifierConfigurationInvalide()
{
    OptionsReservation options = new()
    {
        NombreParticipantsMaximum = 0,
        TarifRegulierParPersonne = 30m,
        AgeMaximumEnfant = 12,
        AgeMinimumAine = 65
    };

    try
    {
        _ = new FournisseurPolitiqueReservationConfiguration(
            Options.Create(options));
        throw new InvalidOperationException(
            "Une configuration invalide aurait dû être refusée.");
    }
    catch (InvalidOperationException exception)
        when (exception.Message == "La configuration des réservations est invalide.")
    {
    }
}

static void VerifierReglesRabais(DateOnly dateReservation)
{
    Participant enfant = new(
        Guid.NewGuid(),
        "Enfant",
        dateReservation.AddYears(-10),
        false);
    Participant etudiante = new(
        Guid.NewGuid(),
        "Étudiante",
        dateReservation.AddYears(-20),
        true);
    Participant ainee = new(
        Guid.NewGuid(),
        "Aînée",
        dateReservation.AddYears(-70),
        false);

    IRegleAdmissibiliteRabais regleEnfant = new RegleRabaisEnfant(
        12,
        Rabais("ENFANT_TEST", NatureRabais.Public, 0.50m));
    IRegleAdmissibiliteRabais regleEtudiant = new RegleRabaisEtudiant(
        Rabais("ETUDIANT_TEST", NatureRabais.Public, 0.15m));
    IRegleAdmissibiliteRabais regleAine = new RegleRabaisAine(
        65,
        Rabais("AINE_TEST", NatureRabais.Public, 0.20m));
    IRegleAdmissibiliteRabais reglePromotion =
        new RegleRabaisInconditionnel(
            Rabais("PROMOTION_TEST", NatureRabais.Promotion, 0.10m));

    ContexteTarificationParticipant contexteEnfant = new(
        enfant,
        dateReservation);
    ContexteTarificationParticipant contexteEtudiante = new(
        etudiante,
        dateReservation);
    ContexteTarificationParticipant contexteAinee = new(
        ainee,
        dateReservation);

    Verifier(regleEnfant.EstAdmissible(contexteEnfant),
        "La règle enfant doit accepter un enfant.");
    Verifier(!regleEnfant.EstAdmissible(contexteEtudiante),
        "La règle enfant doit refuser une adulte.");
    Verifier(regleEtudiant.EstAdmissible(contexteEtudiante),
        "La règle étudiante doit accepter une étudiante.");
    Verifier(regleAine.EstAdmissible(contexteAinee),
        "La règle aînée doit accepter une aînée.");
    Verifier(reglePromotion.EstAdmissible(contexteEtudiante),
        "Une promotion active doit être admissible.");

    VerifierLance<ArgumentOutOfRangeException>(
        () => _ = new RegleRabaisEnfant(-1, regleEnfant.Rabais),
        "Un âge maximal négatif doit être refusé.");
    VerifierLance<ArgumentOutOfRangeException>(
        () => _ = new RegleRabaisAine(-1, regleAine.Rabais),
        "Un âge minimal négatif doit être refusé.");
}

static void VerifierPolitiqueSansRabaisEtDepartage(DateOnly dateReservation)
{
    Participant adulte = new(
        Guid.NewGuid(),
        "Adulte",
        dateReservation.AddYears(-35),
        false);
    PolitiqueReservation sansRabais = new(
        2,
        new Montant(30m),
        Array.Empty<IRegleAdmissibiliteRabais>());

    TarificationParticipant tarificationSansRabais =
        sansRabais.Tarifer(adulte, dateReservation);
    Verifier(tarificationSansRabais.RabaisRetenu.Code == "AUCUN",
        "L'absence de règle admissible doit conserver le tarif régulier.");

    PolitiqueReservation departage = new(
        2,
        new Montant(30m),
        new IRegleAdmissibiliteRabais[]
        {
            new RegleRabaisInconditionnel(
                Rabais("PROMOTION", NatureRabais.Promotion, 0.20m)),
            new RegleRabaisInconditionnel(
                Rabais("PUBLIC", NatureRabais.Public, 0.20m))
        });

    TarificationParticipant tarificationDepartage =
        departage.Tarifer(adulte, dateReservation);
    Verifier(tarificationDepartage.RabaisRetenu.Code == "PUBLIC",
        "Un rabais public doit gagner une égalité avec une promotion.");

    PolitiqueReservation departageAlphabetique = new(
        2,
        new Montant(30m),
        new IRegleAdmissibiliteRabais[]
        {
            new RegleRabaisInconditionnel(
                Rabais("PROMOTION_B", NatureRabais.Promotion, 0.20m)),
            new RegleRabaisInconditionnel(
                Rabais("PROMOTION_A", NatureRabais.Promotion, 0.20m))
        });

    TarificationParticipant tarificationAlphabetique =
        departageAlphabetique.Tarifer(adulte, dateReservation);
    Verifier(tarificationAlphabetique.RabaisRetenu.Code == "PROMOTION_A",
        "Le code alphabétique doit départager deux rabais de même nature.");

    VerifierLance<ArgumentOutOfRangeException>(
        () => _ = new PolitiqueReservation(
            0,
            new Montant(30m),
            Array.Empty<IRegleAdmissibiliteRabais>()),
        "Une capacité nulle doit être refusée.");
    VerifierLance<ArgumentOutOfRangeException>(
        () => _ = new PolitiqueReservation(
            2,
            new Montant(0m),
            Array.Empty<IRegleAdmissibiliteRabais>()),
        "Un tarif régulier nul doit être refusé.");

    VerifierLance<ArgumentException>(
        () => _ = new PolitiqueReservation(
            2,
            new Montant(30m),
            new IRegleAdmissibiliteRabais[]
            {
                new RegleRabaisInconditionnel(
                    Rabais("DOUBLON", NatureRabais.Public, 0.10m)),
                new RegleRabaisInconditionnel(
                    Rabais("DOUBLON", NatureRabais.Promotion, 0.20m))
            }),
        "Deux règles ne doivent pas partager le même code de rabais.");
}

static void VerifierPreconditions(DateOnly dateReservation)
{
    Participant participant = new(
        Guid.NewGuid(),
        "Participant",
        dateReservation.AddYears(-30),
        false);
    RabaisApplicable rabais = Rabais(
        "TEST",
        NatureRabais.Public,
        0.10m);

    VerifierLance<ArgumentNullException>(
        () => _ = new PolitiqueReservation(
            2,
            null!,
            Array.Empty<IRegleAdmissibiliteRabais>()),
        "Une politique doit exiger un tarif régulier.");
    VerifierLance<ArgumentNullException>(
        () => _ = new RabaisApplicable(
            "TEST",
            NatureRabais.Public,
            null!),
        "Un rabais doit exiger un pourcentage.");
    VerifierLance<ArgumentOutOfRangeException>(
        () => _ = new RabaisApplicable(
            "TEST",
            (NatureRabais)999,
            new PourcentageRabais(0.10m)),
        "Une nature de rabais inconnue doit être refusée.");
    VerifierLance<ArgumentNullException>(
        () => _ = new Montant(10m).Appliquer(null!),
        "L'application d'un rabais doit exiger un pourcentage.");
    VerifierLance<ArgumentNullException>(
        () => _ = (Montant)null! + new Montant(10m),
        "L'addition doit exiger deux montants.");
    VerifierLance<ArgumentOutOfRangeException>(
        () => _ = participant.CalculerAgeALaDate(
            participant.DateNaissance.AddDays(-1)),
        "La date de référence ne doit pas précéder la naissance.");
    VerifierLance<ArgumentOutOfRangeException>(
        () => _ = DecisionReservation.Refuser(
            CodeRefusReservation.Aucun),
        "Un refus doit contenir un vrai code de refus.");
    VerifierLance<ArgumentOutOfRangeException>(
        () => _ = ResultatCreationReservation.Refuser(
            (CodeRefusReservation)999),
        "Un résultat doit refuser un code inconnu.");
    VerifierLance<ArgumentException>(
        () => _ = new TarificationParticipant(
            Guid.Empty,
            30,
            Array.Empty<string>(),
            rabais,
            new Montant(30m),
            new Montant(27m)),
        "Une tarification doit exiger l'identifiant du participant.");
    VerifierLance<ArgumentException>(
        () => _ = new TarificationParticipant(
            participant.Id,
            30,
            new[] { "" },
            rabais,
            new Montant(30m),
            new Montant(27m)),
        "Une tarification doit refuser un code de rabais vide.");
    VerifierLance<ArgumentOutOfRangeException>(
        () => _ = new TarificationParticipant(
            participant.Id,
            30,
            new[] { rabais.Code },
            rabais,
            new Montant(30m),
            new Montant(31m)),
        "Le tarif final ne doit pas dépasser le tarif régulier.");

    ResultatCreationReservation creation = Reservation.CreerBrouillon(
        Guid.NewGuid(),
        "Client",
        dateReservation.ToDateTime(TimeOnly.MinValue),
        dateReservation.AddDays(-1).ToDateTime(TimeOnly.MinValue));
    Reservation reservation = creation.Reservation
        ?? throw new InvalidOperationException(
            "La réservation de validation aurait dû être créée.");
    VerifierLance<ArgumentException>(
        () => reservation.RetirerParticipant(Guid.Empty),
        "Le retrait doit exiger l'identifiant du participant.");
}

static void VerifierResultatsCreation(DateTimeOffset maintenant)
{
    ResultatCreationReservation acceptee = Reservation.CreerBrouillon(
        Guid.NewGuid(),
        "Client",
        maintenant.AddDays(1),
        maintenant);
    Verifier(acceptee.EstAcceptee && acceptee.Reservation is not null,
        "Une date future doit produire une réservation.");
    Verifier(acceptee.CodeRefus == CodeRefusReservation.Aucun,
        "Une création acceptée ne doit pas contenir de refus.");

    ResultatCreationReservation refusee = Reservation.CreerBrouillon(
        Guid.NewGuid(),
        "Client",
        maintenant,
        maintenant);
    Verifier(!refusee.EstAcceptee && refusee.Reservation is null,
        "Une création refusée ne doit pas produire de réservation.");
    Verifier(refusee.CodeRefus == CodeRefusReservation.DateNonFuture,
        "Le refus attendu doit indiquer une date non future.");
}

static void VerifierLance<TException>(Action action, string message)
    where TException : Exception
{
    try
    {
        action();
    }
    catch (TException)
    {
        return;
    }

    throw new InvalidOperationException(message);
}

internal sealed class FausseHorloge : IHorloge
{
    public DateTimeOffset Maintenant { get; }

    public FausseHorloge(DateTimeOffset maintenant)
    {
        Maintenant = maintenant;
    }
}

internal sealed class FournisseurPolitiqueFixe
    : IFournisseurPolitiqueReservation
{
    private readonly PolitiqueReservation m_politique;

    public FournisseurPolitiqueFixe(PolitiqueReservation politique)
    {
        m_politique = politique;
    }

    public PolitiqueReservation Obtenir()
    {
        return m_politique;
    }
}
