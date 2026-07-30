using Microsoft.Extensions.Options;

public sealed class InformationDepot
{
    private readonly DepotOptions m_options;

    public InformationDepot(IOptions<DepotOptions> options)
    {
        m_options = options.Value;
    }

    public string ObtenirDescription()
    {
        return $"{m_options.Format}: {m_options.Chemin}";
    }
}
