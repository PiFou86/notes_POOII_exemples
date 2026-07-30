public sealed class CompteBancaire
{
    private decimal m_solde;

    public decimal Solde => m_solde;

    public void Deposer(decimal montant)
    {
        if (montant <= 0m)
        {
            throw new ArgumentException(
                "Le montant doit être positif.",
                nameof(montant));
        }

        m_solde += montant;
    }
}
