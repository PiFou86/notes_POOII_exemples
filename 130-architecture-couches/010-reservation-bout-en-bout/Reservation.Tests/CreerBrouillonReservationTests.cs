using ExempleReservation.Application;
using ExempleReservation.Domaine;
using FluentAssertions;
using Moq;
using Xunit;

namespace ExempleReservation.Tests;

public sealed class CreerBrouillonReservationTests
{
    [Fact]
    public void Executer_DateFutureSelonHorloge_AjouteBrouillon()
    {
        DateTimeOffset maintenant = new(
            2026, 2, 10, 8, 0, 0, TimeSpan.Zero);
        DateTimeOffset datePrevue = maintenant.AddDays(1);
        Mock<IHorloge> mockHorloge = new();
        mockHorloge
            .SetupGet(horloge => horloge.Maintenant)
            .Returns(maintenant);
        Mock<IDepotReservations> mockDepot = new();
        CreerBrouillonReservation casUtilisation = new(
            mockDepot.Object,
            mockHorloge.Object);

        Result<ReservationDto> resultat = casUtilisation.Executer(
            new CreerBrouillonReservationCommande("Alice", datePrevue));

        resultat.EstSucces.Should().BeTrue();
        resultat.Valeur.Should().NotBeNull();
        mockDepot.Verify(
            depot => depot.Ajouter(It.Is<Reservation>(
                reservation => reservation.DatePrevue == datePrevue)),
            Times.Once);
        mockHorloge.VerifyGet(
            horloge => horloge.Maintenant,
            Times.Once);
    }

    [Fact]
    public void Executer_DateEgaleAMaintenant_RefuseSansAjouter()
    {
        DateTimeOffset maintenant = new(
            2026, 2, 10, 8, 0, 0, TimeSpan.Zero);
        Mock<IHorloge> mockHorloge = new();
        mockHorloge
            .SetupGet(horloge => horloge.Maintenant)
            .Returns(maintenant);
        Mock<IDepotReservations> mockDepot = new();
        CreerBrouillonReservation casUtilisation = new(
            mockDepot.Object,
            mockHorloge.Object);

        Result<ReservationDto> resultat = casUtilisation.Executer(
            new CreerBrouillonReservationCommande("Alice", maintenant));

        resultat.EstSucces.Should().BeFalse();
        resultat.MessageErreur.Should().Be(
            "La réservation doit être prévue dans le futur.");
        mockDepot.Verify(
            depot => depot.Ajouter(It.IsAny<Reservation>()),
            Times.Never);
        mockHorloge.VerifyGet(
            horloge => horloge.Maintenant,
            Times.Once);
    }
}
