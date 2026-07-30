public sealed class CreateurExporteurTexte : CreateurExporteur
{
    protected override IExporteur CreerExporteur()
    {
        return new ExporteurTexte();
    }
}
