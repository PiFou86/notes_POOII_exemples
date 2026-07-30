public readonly struct MesureMetre
{
    public double Valeur { get; }

    public MesureMetre(double valeur)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(valeur);
        Valeur = valeur;
    }

    public static MesureMetre operator +(
        MesureMetre gauche,
        MesureMetre droite)
    {
        return new MesureMetre(gauche.Valeur + droite.Valeur);
    }

    public static MesureMetre operator -(
        MesureMetre gauche,
        MesureMetre droite)
    {
        if (droite.Valeur > gauche.Valeur)
        {
            throw new ArgumentOutOfRangeException(
                nameof(droite),
                "La mesure soustraite ne peut pas dépasser la mesure de départ.");
        }

        return new MesureMetre(gauche.Valeur - droite.Valeur);
    }
}
