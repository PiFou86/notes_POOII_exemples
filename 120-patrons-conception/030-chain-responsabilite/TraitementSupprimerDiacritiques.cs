using System.Globalization;
using System.Text;

public sealed class TraitementSupprimerDiacritiques : ITraitementChaineCaracteres
{
    public ITraitementChaineCaracteres? Suivant { get; set; }

    public string Traiter(string chaine)
    {
        ArgumentNullException.ThrowIfNull(chaine);
        string decomposee = chaine.Normalize(NormalizationForm.FormD);
        StringBuilder resultat = new();
        foreach (char caractere in decomposee)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(caractere) != UnicodeCategory.NonSpacingMark)
            {
                resultat.Append(caractere);
            }
        }

        string sansDiacritiques = resultat.ToString().Normalize(NormalizationForm.FormC);
        return Suivant?.Traiter(sansDiacritiques) ?? sansDiacritiques;
    }
}
