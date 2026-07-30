public sealed class CreateurExporteurJson : CreateurExporteur
{
    protected override IExporteur CreerExporteur()
    {
        return new ExporteurJson();
    }
}
