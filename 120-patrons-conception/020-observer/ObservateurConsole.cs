public sealed class ObservateurConsole : IObserver<NoteEvenement>
{
    public bool EstTermine { get; private set; }

    public void OnNext(NoteEvenement value)
    {
        Console.Out.WriteLine($"Note ajoutée: {value.Note}");
    }

    public void OnError(Exception error)
    {
        Console.Error.WriteLine(error.Message);
    }

    public void OnCompleted()
    {
        Console.Out.WriteLine("Observation terminée.");
        EstTermine = true;
    }
}
