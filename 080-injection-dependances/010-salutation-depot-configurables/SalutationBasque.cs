public sealed class SalutationBasque : ISalutation
{
    public string CreerMessage(string? nom = null)
    {
        return nom is null ? "Kaixo" : $"Kaixo {nom}!";
    }
}
