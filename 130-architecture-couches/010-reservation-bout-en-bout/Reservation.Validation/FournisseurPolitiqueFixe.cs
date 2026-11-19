using ExempleReservation.Application;
using ExempleReservation.Domaine;

internal sealed class FournisseurPolitiqueFixe
    : IFournisseurPolitiqueReservation
{
    private readonly PolitiqueReservation m_politique;

    public FournisseurPolitiqueFixe(PolitiqueReservation politique)
    {
        m_politique = politique;
    }

    public PolitiqueReservation Obtenir()
    {
        return m_politique;
    }
}
