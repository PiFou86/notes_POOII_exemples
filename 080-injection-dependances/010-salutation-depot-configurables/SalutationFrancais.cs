public sealed class SalutationFrancais : ISalutation
{
    public string CreerMessage(string? nom = null)
    {
        return nom is null ? "Bonjour" : $"Bonjour {nom} !";
    }
}
