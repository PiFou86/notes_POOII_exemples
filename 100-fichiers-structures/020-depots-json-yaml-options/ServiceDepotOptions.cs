using Microsoft.Extensions.Options;

public sealed class ServiceDepotOptions
{
    private readonly DepotOptions m_options;

    public ServiceDepotOptions(IOptions<DepotOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        m_options = options.Value;
    }

    public string ObtenirCheminDepot()
    {
        return m_options.Chemin;
    }
}
