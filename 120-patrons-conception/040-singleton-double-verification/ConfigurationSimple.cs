public sealed class ConfigurationSimple
{
    private static ConfigurationSimple? s_instance;

    public static ConfigurationSimple Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new ConfigurationSimple();
            }

            return s_instance;
        }
    }

    private ConfigurationSimple() { }
}
