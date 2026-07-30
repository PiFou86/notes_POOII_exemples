public sealed record Rectangle(double Largeur, double Hauteur) : IForme
{
    public double CalculerAire() => Largeur * Hauteur;
}
