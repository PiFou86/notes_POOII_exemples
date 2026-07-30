public sealed record MetriquesDependances(int Ca, int Ce)
{
    public double? Instabilite =>
        Ca + Ce == 0 ? null : (double)Ce / (Ca + Ce);
}
