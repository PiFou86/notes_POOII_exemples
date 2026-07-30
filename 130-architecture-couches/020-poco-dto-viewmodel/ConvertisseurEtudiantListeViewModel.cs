public sealed class ConvertisseurEtudiantListeViewModel
{
    public EtudiantListeViewModel VersViewModel(
        EtudiantDto etudiant,
        bool peutEtreModifie)
    {
        ArgumentNullException.ThrowIfNull(etudiant);
        return new EtudiantListeViewModel
        {
            NomAffiche = etudiant.Nom,
            CourrielAffiche = $"Courriel : {etudiant.Courriel}",
            PeutEtreModifie = peutEtreModifie
        };
    }
}
