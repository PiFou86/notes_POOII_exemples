public abstract class GestionnaireApprobation
{
    public GestionnaireApprobation? Suivant { get; set; }

    public void Traiter(Demande demande)
    {
        ArgumentNullException.ThrowIfNull(demande);
        if (PeutTraiter(demande))
        {
            Approuver(demande);
            return;
        }

        if (Suivant is null)
        {
            throw new InvalidOperationException(
                "Aucun gestionnaire ne peut traiter la demande.");
        }

        Suivant.Traiter(demande);
    }

    protected abstract bool PeutTraiter(Demande demande);
    protected abstract void Approuver(Demande demande);
}
