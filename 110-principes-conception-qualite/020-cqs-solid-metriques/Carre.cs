public sealed record Carre(double Cote) : IForme
{
    public double CalculerAire() => Cote * Cote;
}
