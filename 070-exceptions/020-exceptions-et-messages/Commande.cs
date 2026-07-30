public sealed class Commande
{
    public bool EstFacturee { get; private set; }

    public void Facturer()
    {
        EstFacturee = true;
    }

    public void AjouterLigne()
    {
        if (EstFacturee)
        {
            throw new CommandeDansEtatInvalideException(
                "Une ligne ne peut pas être ajoutée à une commande facturée.");
        }
    }
}
