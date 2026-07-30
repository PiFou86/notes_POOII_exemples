using ExempleReservation.Application;
using ExempleReservation.Domaine;
using Microsoft.Extensions.Options;

namespace ExempleReservation.Infrastructure;

public sealed class FournisseurPolitiqueReservationConfiguration
    : IFournisseurPolitiqueReservation
{
    private readonly PolitiqueReservation m_politique;

    public FournisseurPolitiqueReservationConfiguration(
        IOptions<OptionsReservation> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        try
        {
            m_politique = CreerPolitique(options.Value);
        }
        catch (Exception exception) when (
            exception is ArgumentException or InvalidOperationException)
        {
            throw new InvalidOperationException(
                "La configuration des réservations est invalide.",
                exception);
        }
    }

    public PolitiqueReservation Obtenir()
    {
        return m_politique;
    }

    private static PolitiqueReservation CreerPolitique(
        OptionsReservation options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(options.RabaisPublic);
        ArgumentNullException.ThrowIfNull(options.PromotionsActives);

        if (options.PromotionsActives.Any(promotion => promotion is null))
        {
            throw new ArgumentException(
                "Une promotion active ne peut pas être nulle.",
                nameof(options));
        }

        List<IRegleAdmissibiliteRabais> reglesRabais = new()
        {
            new RegleRabaisEnfant(
                options.AgeMaximumEnfant,
                CreerRabaisPublic("ENFANT", options.RabaisPublic.Enfant)),
            new RegleRabaisEtudiant(
                CreerRabaisPublic("ETUDIANT", options.RabaisPublic.Etudiant)),
            new RegleRabaisAine(
                options.AgeMinimumAine,
                CreerRabaisPublic("AINE", options.RabaisPublic.Aine))
        };

        reglesRabais.AddRange(options.PromotionsActives.Select(
            promotion => new RegleRabaisInconditionnel(
                new RabaisApplicable(
                    promotion.Code,
                    NatureRabais.Promotion,
                    new PourcentageRabais(promotion.Pourcentage)))));

        return new PolitiqueReservation(
            options.NombreParticipantsMaximum,
            new Montant(options.TarifRegulierParPersonne),
            reglesRabais);
    }

    private static RabaisApplicable CreerRabaisPublic(
        string code,
        decimal pourcentage)
    {
        return new RabaisApplicable(
            code,
            NatureRabais.Public,
            new PourcentageRabais(pourcentage));
    }
}
