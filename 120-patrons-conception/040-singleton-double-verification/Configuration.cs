public sealed class Configuration
{
    private static volatile Configuration? s_instance;
    private static readonly object s_verrou = new();

    public static Configuration Instance
    {
        get
        {
            Configuration? instance = s_instance;
            if (instance == null)
            {
                lock (s_verrou)
                {
                    instance = s_instance;
                    if (instance == null)
                    {
                        instance = new Configuration();
                        s_instance = instance;
                    }
                }
            }

            return instance;
        }
    }

    public string CheminJournal { get; } = "application.log";
    private Configuration() { }
}
