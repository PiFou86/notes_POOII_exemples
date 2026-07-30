public sealed class Matrice
{
    private readonly double[,] m_valeurs;

    public int NombreLignes
    {
        get
        {
            return m_valeurs.GetLength(0);
        }
    }

    public int NombreColonnes
    {
        get
        {
            return m_valeurs.GetLength(1);
        }
    }

    public Matrice(int nombreLignes, int nombreColonnes)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(nombreLignes);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(nombreColonnes);
        m_valeurs = new double[nombreLignes, nombreColonnes];
    }

    public double this[int ligne, int colonne]
    {
        get
        {
            ValiderIndex(ligne, colonne);
            return m_valeurs[ligne, colonne];
        }
        set
        {
            ValiderIndex(ligne, colonne);
            m_valeurs[ligne, colonne] = value;
        }
    }

    private void ValiderIndex(int ligne, int colonne)
    {
        if (ligne < 0 || ligne >= NombreLignes)
        {
            throw new ArgumentOutOfRangeException(nameof(ligne));
        }

        if (colonne < 0 || colonne >= NombreColonnes)
        {
            throw new ArgumentOutOfRangeException(nameof(colonne));
        }
    }
}
