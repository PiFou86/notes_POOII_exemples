using System.Text.Json;

public sealed class ExporteurJson : IExporteur
{
    public string Exporter(string contenu)
    {
        return JsonSerializer.Serialize(contenu);
    }
}
