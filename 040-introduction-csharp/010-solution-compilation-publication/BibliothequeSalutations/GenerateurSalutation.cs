namespace BibliothequeSalutations;

public sealed class GenerateurSalutation
{
    public string Creer(string nom)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nom);
        return $"Bonjour {nom.Trim()} !";
    }
}
