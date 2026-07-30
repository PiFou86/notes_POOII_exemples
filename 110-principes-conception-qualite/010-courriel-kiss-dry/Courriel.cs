public sealed class Courriel : IEquatable<Courriel>
{
    private readonly string m_valeur;

    public string Valeur
    {
        get { return m_valeur; }
    }

    public Courriel(string valeur)
    {
        if (string.IsNullOrWhiteSpace(valeur))
        {
            throw new ArgumentException("Courriel invalide.");
        }

        string valeurNormalisee = valeur.Trim();
        int positionArobase = valeurNormalisee.IndexOf('@');
        bool arobaseUnique =
            positionArobase == valeurNormalisee.LastIndexOf('@');

        if (positionArobase <= 0
            || positionArobase >= valeurNormalisee.Length - 1
            || !arobaseUnique)
        {
            throw new ArgumentException("Courriel invalide.");
        }

        m_valeur = valeurNormalisee;
    }

    public bool Equals(Courriel? autre)
    {
        return autre != null
            && StringComparer.OrdinalIgnoreCase.Equals(
                m_valeur,
                autre.m_valeur);
    }

    public override bool Equals(object? objet)
    {
        return Equals(objet as Courriel);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(m_valeur);
    }

    public override string ToString()
    {
        return m_valeur;
    }
}
