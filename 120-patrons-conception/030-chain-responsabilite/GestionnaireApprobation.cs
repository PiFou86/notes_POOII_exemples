public abstract class GestionnaireApprobation
{
    private GestionnaireApprobation? m_suivant;

    public GestionnaireApprobation? Suivant
    {
        get => m_suivant;
        set => m_suivant = value;
    }

    public void Traiter(Demande demande)
    {
        ArgumentNullException.ThrowIfNull(demande);
        if (PeutTraiter(demande))
        {
            Approuver(demande);
            return;
        }

        if (m_suivant is null)
        {
            throw new InvalidOperationException(
                "Aucun gestionnaire ne peut traiter la demande.");
        }

        m_suivant.Traiter(demande);
    }

    protected abstract bool PeutTraiter(Demande demande);
    protected abstract void Approuver(Demande demande);
}
