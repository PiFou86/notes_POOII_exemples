public sealed class Cours
{
    public string Code { get; }
    public string Titre { get; }

    public Cours(string code, string titre)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(titre);
        Code = code.Trim().ToUpperInvariant();
        Titre = titre.Trim();
    }

    public override string ToString() => $"{Code} - {Titre}";

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj)
            || obj is Cours autre && Code == autre.Code && Titre == autre.Titre;
    }

    public override int GetHashCode() => HashCode.Combine(Code, Titre);
}
