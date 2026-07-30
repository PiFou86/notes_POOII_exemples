public sealed class ConvertisseurEtudiant
{
    public EtudiantDto VersDto(Etudiant etudiant)
    {
        ArgumentNullException.ThrowIfNull(etudiant);
        return new EtudiantDto
        {
            CodePermanent = etudiant.CodePermanent.Valeur,
            Nom = etudiant.Nom,
            Courriel = etudiant.Courriel.Valeur
        };
    }

    public Etudiant VersEntite(EtudiantDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        return new Etudiant(
            new CodePermanent(dto.CodePermanent),
            dto.Nom,
            new Courriel(dto.Courriel));
    }
}
