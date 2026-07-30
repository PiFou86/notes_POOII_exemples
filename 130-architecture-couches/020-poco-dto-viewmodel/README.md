# CH13-E02 — Distinguer POCO, DTO et ViewModel

Source : `chapitres/130-architecture-couches.tex`, section
`sec:poco-dto-viewmodel`. Type : console autonome.

Les types sont volontairement réunis dans un petit projet afin de comparer
leurs rôles sans répéter l'exemple multicouche CH13-E01 :

- `Etudiant` est une entité métier et un POCO;
- `EtudiantDto` transporte les données d'une frontière applicative;
- `EtudiantListeViewModel` prépare les données d'une vue précise;
- `ConvertisseurEtudiant` convertit dans les deux sens entre l'entité et le DTO;
- `ConvertisseurEtudiantListeViewModel` adapte le DTO à la vue sans mélanger
  cette responsabilité de présentation à la conversion du domaine.

Les trois premiers objets restent des objets CLR ordinaires. Le fait qu'ils
puissent tous être considérés comme des POCO ne leur donne pas la même
responsabilité architecturale.

```bash
dotnet build RolesArchitecture.csproj
dotnet run --project RolesArchitecture.csproj
```
