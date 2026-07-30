public sealed class JournalMemoire
{
    public Exception? DerniereException { get; private set; }

    public void Erreur(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);
        DerniereException = exception;
    }
}
