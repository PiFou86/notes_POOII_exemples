public sealed class RapportBuilder
{
    private string m_titre = "Sans titre";
    private readonly List<string> m_sections = [];

    public RapportBuilder AvecTitre(string titre)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(titre);
        m_titre = titre.Trim();
        return this;
    }

    public RapportBuilder AjouterSection(string section)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(section);
        m_sections.Add(section.Trim());
        return this;
    }

    public Rapport Construire()
    {
        return new Rapport(m_titre, m_sections.AsReadOnly());
    }
}
