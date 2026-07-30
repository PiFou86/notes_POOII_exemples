namespace ExempleReservation.Domaine;

public sealed class Participant
{
    public Guid Id { get; }
    public string Nom { get; }
    public DateOnly DateNaissance { get; }
    public bool EstEtudiant { get; }

    public Participant(
        Guid id,
        string nom,
        DateOnly dateNaissance,
        bool estEtudiant)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(
                "L'identifiant est obligatoire.",
                nameof(id));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(nom);

        Id = id;
        Nom = nom.Trim();
        DateNaissance = dateNaissance;
        EstEtudiant = estEtudiant;
    }

    public int CalculerAgeALaDate(DateOnly dateReference)
    {
        if (dateReference < DateNaissance)
        {
            throw new ArgumentOutOfRangeException(
                nameof(dateReference),
                "La date de référence ne peut pas précéder la naissance.");
        }

        int age = dateReference.Year - DateNaissance.Year;

        if (DateNaissance.AddYears(age) > dateReference)
        {
            age--;
        }

        return age;
    }
}
