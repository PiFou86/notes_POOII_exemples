using System.Runtime.CompilerServices;

public static class LectureProgressive
{
    public static async IAsyncEnumerable<string> LireLignesAsync(
        string chemin,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using StreamReader lecteur = new(chemin);
        while (await lecteur.ReadLineAsync(cancellationToken) is { } ligne)
        {
            yield return ligne;
        }
    }
}
