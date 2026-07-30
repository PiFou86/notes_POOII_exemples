public sealed class Facture
{
    private readonly decimal m_montantBrut;
    private readonly IRabaisStrategy m_rabaisStrategy;

    public Facture(decimal montantBrut, IRabaisStrategy rabaisStrategy)
    {
        m_montantBrut = montantBrut;
        m_rabaisStrategy = rabaisStrategy;
    }

    public decimal CalculerTotal()
    {
        return m_montantBrut - m_rabaisStrategy.CalculerRabais(m_montantBrut);
    }
}
