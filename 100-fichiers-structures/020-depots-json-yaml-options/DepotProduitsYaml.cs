using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public sealed class DepotProduitsYaml : DepotProduitsFichier
{
    private static readonly ISerializer s_serializer =
        new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
    private static readonly IDeserializer s_deserializer =
        new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

    public DepotProduitsYaml(string chemin)
        : base(chemin)
    {
    }

    protected override List<ProduitPersistanceDto> LireDtos()
    {
        if (!File.Exists(Chemin))
        {
            return [];
        }

        string yaml = File.ReadAllText(Chemin, Encoding.UTF8);
        return s_deserializer.Deserialize<List<ProduitPersistanceDto>>(yaml)
            ?? [];
    }

    protected override void EcrireDtos(List<ProduitPersistanceDto> produits)
    {
        string yaml = s_serializer.Serialize(produits);
        File.WriteAllText(Chemin, yaml, Encoding.UTF8);
    }
}
