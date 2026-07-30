Configuration[] instances = await Task.WhenAll(
    Enumerable.Range(0, 100).Select(_ => Task.Run(() => Configuration.Instance)));
Configuration premiere = instances[0];
if (instances.Any(instance => !ReferenceEquals(instance, premiere)))
{
    throw new InvalidOperationException("Le Singleton devrait fournir une seule instance.");
}

if (!ReferenceEquals(ConfigurationSimple.Instance, ConfigurationSimple.Instance)
    || !ReferenceEquals(ConfigurationAvide.Instance, ConfigurationAvide.Instance))
{
    throw new InvalidOperationException("Chaque variante devrait conserver son instance.");
}

Console.Out.WriteLine("Validation réussie.");
