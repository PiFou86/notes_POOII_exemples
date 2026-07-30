public sealed class ServiceEtudiantsMemoire : IServiceEtudiants
{
    private readonly EtudiantDto m_etudiant;

    public ServiceEtudiantsMemoire(EtudiantDto etudiant)
    {
        ArgumentNullException.ThrowIfNull(etudiant);
        m_etudiant = etudiant;
    }

    public EtudiantDto Obtenir(Guid id)
    {
        if (id != m_etudiant.Id)
        {
            throw new KeyNotFoundException("Étudiant introuvable.");
        }

        return m_etudiant;
    }
}
