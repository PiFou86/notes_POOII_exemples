using ExempleReservation.Application;
using ExempleReservation.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservation.WinForms;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        HostApplicationBuilder builder =
            Host.CreateApplicationBuilder(args);

        builder.Services.AddScoped<IDepotReservations,
            DepotReservationsMemoire>();
        builder.Services.AddScoped<IHorloge, HorlogeSysteme>();
        builder.Services.AddScoped<CreerBrouillonReservation>();
        builder.Services.AddScoped<FormReservation>();

        using IHost host = builder.Build();
        using IServiceScope scope = host.Services.CreateScope();

        FormReservation fReservation = scope.ServiceProvider
            .GetRequiredService<FormReservation>();

        Application.Run(fReservation);
    }
}
