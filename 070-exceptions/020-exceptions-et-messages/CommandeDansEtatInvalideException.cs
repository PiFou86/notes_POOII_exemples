public sealed class CommandeDansEtatInvalideException : InvalidOperationException
{
    public CommandeDansEtatInvalideException(string message)
        : base(message)
    {
        ;
    }
}
