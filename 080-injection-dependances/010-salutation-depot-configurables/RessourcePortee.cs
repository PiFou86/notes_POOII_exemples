public sealed class RessourcePortee : IDisposable
{
    public bool EstLiberee { get; private set; }

    public void Dispose()
    {
        EstLiberee = true;
    }
}
