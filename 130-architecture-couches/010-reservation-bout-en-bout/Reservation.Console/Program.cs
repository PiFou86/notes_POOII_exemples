using ExempleReservation.Application;
using ExempleReservation.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.Configure<OptionsReservation>(
    builder.Configuration.GetRequiredSection("Reservations"));

builder.Services.AddSingleton<IDepotReservations, DepotReservationsMemoire>();
builder.Services.AddSingleton<IHorloge, HorlogeSysteme>();
builder.Services.AddSingleton<
    IFournisseurPolitiqueReservation,
    FournisseurPolitiqueReservationConfiguration>();
builder.Services.AddScoped<CreerBrouillonReservation>();
builder.Services.AddScoped<AjouterParticipantReservation>();
builder.Services.AddScoped<RetirerParticipantReservation>();
builder.Services.AddScoped<ConfirmerReservation>();

using IHost host = builder.Build();
using IServiceScope scope = host.Services.CreateScope();
IServiceProvider services = scope.ServiceProvider;

_ = services.GetRequiredService<IFournisseurPolitiqueReservation>().Obtenir();
CreerBrouillonReservation creer =
    services.GetRequiredService<CreerBrouillonReservation>();
AjouterParticipantReservation ajouter =
    services.GetRequiredService<AjouterParticipantReservation>();
RetirerParticipantReservation retirer =
    services.GetRequiredService<RetirerParticipantReservation>();
ConfirmerReservation confirmer =
    services.GetRequiredService<ConfirmerReservation>();

DateTimeOffset datePrevue = DateTimeOffset.Now.AddMonths(1);
ReservationDto reservation = ExigerSucces(creer.Executer(
    new CreerBrouillonReservationCommande("Alice Tremblay", datePrevue)));

reservation = Ajouter("Émile", datePrevue.AddYears(-10), false);
reservation = Ajouter("Sofia", datePrevue.AddYears(-20), true);
reservation = Ajouter("Martin", datePrevue.AddYears(-35), false);
Guid adulteId = reservation.Participants.Single(p => p.Nom == "Martin").Id;
reservation = Ajouter("Jeanne", datePrevue.AddYears(-70), false);

reservation = ExigerSucces(retirer.Executer(
    new RetirerParticipantReservationCommande(reservation.Id, adulteId)));
reservation = Ajouter("Nora", datePrevue.AddYears(-40), false);

reservation = ExigerSucces(confirmer.Executer(
    new ConfirmerReservationCommande(reservation.Id)));

Console.Out.WriteLine(
    $"Réservation {reservation.Id} - {reservation.Etat}");
foreach (TarificationParticipantDto tarif in reservation.Tarifications)
{
    ParticipantDto participant = reservation.Participants.Single(
        p => p.Id == tarif.ParticipantId);
    Console.Out.WriteLine(
        $"{participant.Nom}, {tarif.Age} ans: "
        + $"{tarif.CodeRabaisRetenu} "
        + $"({tarif.PourcentageRabais:P0}), "
        + $"{tarif.TarifFinal:C}");
}

Console.Out.WriteLine($"Total avant taxes: {reservation.Total:C}");
return;

ReservationDto Ajouter(
    string nom,
    DateTimeOffset dateNaissance,
    bool estEtudiant)
{
    return ExigerSucces(ajouter.Executer(
        new AjouterParticipantReservationCommande(
            reservation.Id,
            nom,
            DateOnly.FromDateTime(dateNaissance.Date),
            estEtudiant)));
}

static T ExigerSucces<T>(Result<T> resultat)
{
    if (!resultat.EstSucces || resultat.Valeur is null)
    {
        throw new InvalidOperationException(resultat.MessageErreur);
    }

    return resultat.Valeur;
}
