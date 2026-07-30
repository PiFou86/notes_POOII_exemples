public sealed class Rectangle : Forme
{
    public double Largeur { get; set; }
    public double Hauteur { get; set; }
    public override double CalculerAire() => Largeur * Hauteur;
}
