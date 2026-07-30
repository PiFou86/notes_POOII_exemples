public sealed class CoursObservable : IObservable<NoteEvenement>
{
    private readonly List<IObserver<NoteEvenement>> m_observateurs =
        new List<IObserver<NoteEvenement>>();

    public IDisposable Subscribe(IObserver<NoteEvenement> observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        if (!m_observateurs.Contains(observer))
        {
            m_observateurs.Add(observer);
        }

        return new DesabonnementObservable(m_observateurs, observer);
    }

    public void AjouterNote(int note)
    {
        NoteEvenement evenement = new NoteEvenement(note);
        IObserver<NoteEvenement>[] observateurs =
            m_observateurs.ToArray();
        Array.ForEach(
            observateurs,
            observer => observer.OnNext(evenement));
    }

    public void SignalerErreur(Exception erreur)
    {
        ArgumentNullException.ThrowIfNull(erreur);

        foreach (IObserver<NoteEvenement> observer in m_observateurs.ToArray())
        {
            observer.OnError(erreur);
        }

        m_observateurs.Clear();
    }

    public void TerminerObservation()
    {
        foreach (IObserver<NoteEvenement> observer in m_observateurs.ToArray())
        {
            observer.OnCompleted();
        }

        m_observateurs.Clear();
    }
}
