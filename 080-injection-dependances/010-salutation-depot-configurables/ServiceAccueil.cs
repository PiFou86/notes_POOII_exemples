public sealed class ServiceAccueil
{
    private readonly ISalutation m_salutation;

    public ServiceAccueil(ISalutation salutation)
    {
        m_salutation = salutation;
    }

    public string Saluer(string? nom = null)
    {
        return m_salutation.CreerMessage(nom);
    }
}
