public sealed class Produit
{
    public string Nom
    {
        get;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Le nom est obligatoire.");
            }

            field = value.Trim();
        }
    } = "";
}
