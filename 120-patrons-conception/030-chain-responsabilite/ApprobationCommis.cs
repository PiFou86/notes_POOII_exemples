public sealed class ApprobationCommis : GestionnaireApprobation
{
    protected override bool PeutTraiter(Demande demande) => demande.Montant < 100m;
    protected override void Approuver(Demande demande) =>
        Console.Out.WriteLine("Demande approuvée par un commis.");
}
