public sealed class Cours
{
    public event Action<string>? NoteAjoutee;

    public void AjouterNote(int note)
    {
        NoteAjoutee?.Invoke($"Note ajoutée: {note}");
    }
}
