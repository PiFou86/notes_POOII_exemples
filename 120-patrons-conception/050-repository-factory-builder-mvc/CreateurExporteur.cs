public abstract class CreateurExporteur
{
    public string Exporter(string contenu)
    {
        IExporteur exporteur = CreerExporteur();
        return exporteur.Exporter(contenu);
    }

    protected abstract IExporteur CreerExporteur();
}
