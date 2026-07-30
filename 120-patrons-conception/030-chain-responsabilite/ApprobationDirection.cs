public sealed class ApprobationDirection : GestionnaireApprobation
{
    protected override bool PeutTraiter(Demande demande) => true;
    protected override void Approuver(Demande demande) =>
        Console.Out.WriteLine("Demande approuvée par la direction.");
}
