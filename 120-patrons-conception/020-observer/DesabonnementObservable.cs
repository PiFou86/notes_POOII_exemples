public sealed class DesabonnementObservable : IDisposable
{
    private readonly List<IObserver<NoteEvenement>> m_observateurs;
    private readonly IObserver<NoteEvenement> m_observateur;

    public DesabonnementObservable(
        List<IObserver<NoteEvenement>> observateurs,
        IObserver<NoteEvenement> observateur)
    {
        m_observateurs = observateurs;
        m_observateur = observateur;
    }

    public void Dispose()
    {
        m_observateurs.Remove(m_observateur);
    }
}
