public sealed class ConfigurationAvide
{
    private static readonly ConfigurationAvide s_instance = new();
    public static ConfigurationAvide Instance => s_instance;
    private ConfigurationAvide() { }
}
