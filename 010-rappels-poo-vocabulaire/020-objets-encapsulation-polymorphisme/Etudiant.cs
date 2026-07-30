public sealed class Etudiant
{
    public string Nom { get; }
    public int NombreCoursReussis { get; private set; }

    public Etudiant(string nom)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nom);
        Nom = nom.Trim();
    }

    public void ReussirUnCours() => NombreCoursReussis++;
}
