public sealed class ObservateurValidation : IObserver<NoteEvenement>
{
    public List<int> NotesRecues { get; } = [];
    public Exception? ErreurRecue { get; private set; }

    public void OnNext(NoteEvenement value)
    {
        NotesRecues.Add(value.Note);
    }

    public void OnError(Exception error)
    {
        ErreurRecue = error;
    }

    public void OnCompleted()
    {
    }
}
