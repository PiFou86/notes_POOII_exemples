public sealed class ObservateurAction<T> : IObserver<T>, IDisposable
{
    private readonly Action<T> m_reagir;
    private readonly Action<Exception>? m_traiterErreur;
    private readonly Action? m_terminer;
    private IDisposable? m_abonnement;

    public ObservateurAction(
        IObservable<T> sujet,
        Action<T> reagir,
        Action<Exception>? traiterErreur = null,
        Action? terminer = null)
    {
        ArgumentNullException.ThrowIfNull(sujet);
        ArgumentNullException.ThrowIfNull(reagir);

        m_reagir = reagir;
        m_traiterErreur = traiterErreur;
        m_terminer = terminer;
        m_abonnement = sujet.Subscribe(this);
    }

    public void OnNext(T valeur)
    {
        m_reagir(valeur);
    }

    public void OnError(Exception erreur)
    {
        m_traiterErreur?.Invoke(erreur);
    }

    public void OnCompleted()
    {
        m_terminer?.Invoke();
    }

    public void Dispose()
    {
        m_abonnement?.Dispose();
        m_abonnement = null;
    }
}
