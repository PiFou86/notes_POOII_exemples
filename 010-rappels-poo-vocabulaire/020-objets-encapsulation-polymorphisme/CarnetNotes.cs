public sealed class CarnetNotes
{
    private readonly List<int> m_notes = new List<int>();

    public void Ajouter(int note)
    {
        if (note is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(note));
        }

        m_notes.Add(note);
    }

    public List<int> ObtenirNotes()
    {
        return new List<int>(m_notes);
    }
}
