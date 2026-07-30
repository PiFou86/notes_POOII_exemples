Etudiant etudiant = new(
    new CodePermanent("trea01010101"),
    "Alice Tremblay",
    new Courriel("Alice@Example.Test"));

ConvertisseurEtudiant convertisseur = new();
EtudiantDto dto = convertisseur.VersDto(etudiant);
Etudiant entiteReconstruite = convertisseur.VersEntite(dto);

ConvertisseurEtudiantListeViewModel convertisseurViewModel = new();
EtudiantListeViewModel viewModel =
    convertisseurViewModel.VersViewModel(dto, true);

if (etudiant.Courriel.Valeur != "alice@example.test")
{
    throw new InvalidOperationException("L'entité devrait protéger ses valeurs métier.");
}

if (dto.CodePermanent != "TREA01010101"
    || entiteReconstruite.CodePermanent.Valeur != dto.CodePermanent
    || !viewModel.PeutEtreModifie)
{
    throw new InvalidOperationException("Les objets devraient être adaptés à chaque frontière.");
}

Console.Out.WriteLine(viewModel.NomAffiche);
Console.Out.WriteLine(viewModel.CourrielAffiche);
