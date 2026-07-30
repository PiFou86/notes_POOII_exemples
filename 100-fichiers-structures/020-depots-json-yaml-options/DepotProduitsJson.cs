using System.Text;
using System.Text.Json;

public sealed class DepotProduitsJson : DepotProduitsFichier
{
    private static readonly JsonSerializerOptions s_options =
        new(JsonSerializerDefaults.Web)
        {
            WriteIndented = true
        };

    public DepotProduitsJson(string chemin)
        : base(chemin)
    {
    }

    protected override List<ProduitPersistanceDto> LireDtos()
    {
        if (!File.Exists(Chemin))
        {
            return [];
        }

        string json = File.ReadAllText(Chemin, Encoding.UTF8);
        return JsonSerializer.Deserialize<List<ProduitPersistanceDto>>(
            json,
            s_options) ?? [];
    }

    protected override void EcrireDtos(List<ProduitPersistanceDto> produits)
    {
        string json = JsonSerializer.Serialize(produits, s_options);
        File.WriteAllText(Chemin, json, Encoding.UTF8);
    }
}
