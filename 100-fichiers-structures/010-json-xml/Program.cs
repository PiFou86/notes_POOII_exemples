using System.Text.Json;
using System.Xml.Serialization;

JsonSerializerOptions options = new(JsonSerializerDefaults.Web) { WriteIndented = true };
List<Produit> produits =
[
    new Produit
    {
        Id = 1,
        Nom = "Clavier",
        Categorie = "Informatique"
    }
];
string json = JsonSerializer.Serialize(produits, options);
List<Produit> produitsRelus =
    JsonSerializer.Deserialize<List<Produit>>(json, options) ?? [];

XmlSerializer serializer = new(typeof(List<Produit>));
using MemoryStream flux = new();
serializer.Serialize(flux, produitsRelus);
flux.Position = 0;
List<Produit> produitsRelusXml =
    serializer.Deserialize(flux) as List<Produit> ?? [];

if (produitsRelusXml.Count != 1 || produitsRelusXml[0].Nom != "Clavier")
{
    throw new InvalidOperationException("Les données relues ne correspondent pas.");
}

Console.Out.WriteLine("Validation réussie.");
