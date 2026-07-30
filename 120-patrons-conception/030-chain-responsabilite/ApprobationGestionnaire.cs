public sealed class ApprobationGestionnaire : GestionnaireApprobation
{
    protected override bool PeutTraiter(Demande demande)
    {
        return demande.Montant < 1_000m;
    }

    protected override void Approuver(Demande demande)
    {
        Console.Out.WriteLine("Demande approuvée par un gestionnaire.");
    }
}
