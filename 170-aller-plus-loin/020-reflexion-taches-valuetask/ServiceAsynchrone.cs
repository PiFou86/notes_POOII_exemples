public sealed class ServiceAsynchrone
{
    private readonly Dictionary<int, string> m_cache = new() { [1] = "Alice" };
    public async Task AttendreAsync(CancellationToken jeton) => await Task.Delay(1, jeton);
    public async Task<int> CalculerAsync() { await Task.Yield(); return 42; }
    public ValueTask<string?> ObtenirNomAsync(int id) => ValueTask.FromResult(m_cache.GetValueOrDefault(id));
}
