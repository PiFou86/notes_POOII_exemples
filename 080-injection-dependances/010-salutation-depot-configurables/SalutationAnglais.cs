public sealed class SalutationAnglais : ISalutation
{
    public string CreerMessage(string? nom = null)
    {
        return nom is null ? "Hello" : $"Hello {nom}!";
    }
}
