using ExempleReservation.Application;

namespace Reservation.WinForms;

public sealed class FormReservation : Form
{
    private readonly CreerBrouillonReservation m_creerReservation;

    private readonly TextBox tbNom = new()
    {
        Anchor = AnchorStyles.Left | AnchorStyles.Right,
        Name = "tbNom"
    };

    private readonly DateTimePicker dtpDate = new()
    {
        Anchor = AnchorStyles.Left | AnchorStyles.Right,
        Name = "dtpDate",
        MinDate = DateTime.Today.AddDays(1)
    };

    private readonly Button bCreer = new()
    {
        AutoSize = true,
        Name = "bCreer",
        Text = "&Créer"
    };

    private readonly Label lResultat = new()
    {
        AutoSize = true,
        Name = "lResultat"
    };

    public FormReservation(CreerBrouillonReservation creerReservation)
    {
        ArgumentNullException.ThrowIfNull(creerReservation);
        m_creerReservation = creerReservation;

        Name = "fReservation";
        Text = "Réservation";
        MinimumSize = new Size(420, 230);
        StartPosition = FormStartPosition.CenterScreen;

        TableLayoutPanel tlpPrincipal = CreerDisposition();
        Controls.Add(tlpPrincipal);

        AcceptButton = bCreer;
        bCreer.Click += BCreer_Click;
    }

    private TableLayoutPanel CreerDisposition()
    {
        TableLayoutPanel tlpPrincipal = new()
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Name = "tlpPrincipal",
            Padding = new Padding(12),
            RowCount = 4
        };

        tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tlpPrincipal.Controls.Add(new Label { AutoSize = true, Text = "&Nom :" }, 0, 0);
        tlpPrincipal.Controls.Add(tbNom, 1, 0);
        tlpPrincipal.Controls.Add(new Label { AutoSize = true, Text = "&Date :" }, 0, 1);
        tlpPrincipal.Controls.Add(dtpDate, 1, 1);
        tlpPrincipal.Controls.Add(bCreer, 1, 2);
        tlpPrincipal.Controls.Add(lResultat, 0, 3);
        tlpPrincipal.SetColumnSpan(lResultat, 2);
        return tlpPrincipal;
    }

    private void BCreer_Click(object? sender, EventArgs e)
    {
        DateTime dateLocale = dtpDate.Value.Date;
        DateTimeOffset datePrevue = new(dateLocale, TimeZoneInfo.Local.GetUtcOffset(dateLocale));
        Result<ReservationDto> resultat = m_creerReservation.Executer(
            new CreerBrouillonReservationCommande(tbNom.Text, datePrevue));

        if (!resultat.EstSucces || resultat.Valeur is null)
        {
            lResultat.Text = resultat.MessageErreur ?? "La réservation n'a pas été créée.";
            tbNom.Focus();
            return;
        }

        lResultat.Text = $"Brouillon créé pour {resultat.Valeur.NomClient}.";
    }
}
