# CH12-E06 — Comparer MVC Web et MVVM

Source : `chapitres/120-patrons-conception.tex`, section
`sec:mvc-mvvm`. Type : console autonome avec référence au cadriciel
ASP.NET Core partagé par .NET.

Le scénario ne démarre aucun serveur ni aucune fenêtre. Il rend seulement les
collaborations POO testables et observables :

- un contrôleur MVC Web obtient un DTO, construit un ViewModel et retourne une
  `ViewResult`;
- un ViewModel MVVM expose un état de présentation et publie ses changements
  avec `INotifyPropertyChanged`.

Le MVC interactif simplifié demeure dans CH12-E05. Le présent projet montre
surtout les deux usages du terme ViewModel présentés dans les notes.

```bash
dotnet build PatronsPresentation.csproj
dotnet run --project PatronsPresentation.csproj
```
