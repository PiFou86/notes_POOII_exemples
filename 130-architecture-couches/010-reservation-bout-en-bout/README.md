# CH13-E01 — Créer une réservation de bout en bout

Source : `chapitres/130-architecture-couches.tex`, section
`sec:architecture-exemple-bout-en-bout`. Type : architecture complète.

L'exemple sépare une présentation console, quatre cas d'utilisation Application,
un agrégat du domaine et trois adaptateurs Infrastructure. Une réservation conserve
un brouillon, des participants détaillés et un instantané tarifaire. La politique
vient d'`appsettings.json`, mais Le domaine calcule l'âge et le meilleur rabais au
moyen d'une collection de règles d'admissibilité. La création du brouillon
retourne un résultat métier dédié plutôt qu'un paramètre `out`.
Les constructeurs et les méthodes publiques vérifient leurs préconditions;
les erreurs de programmation lancent une exception d'argument, tandis que les
refus métier attendus restent représentés par un résultat.

La console est la racine de composition construite avec
`HostApplicationBuilder`. Le projet `Reservation.Validation` injecte plutôt un
dépôt mémoire, une fausse horloge et une politique fixe afin de valider le
scénario sans démarrer le host. Le projet `Reservation.Tests` montre ensuite
deux tests xUnit avec Moq : l'horloge fixe la date de référence et le dépôt
permet de vérifier si une réservation a été ajoutée.

```bash
dotnet build Reservation.Console/Reservation.Console.csproj
dotnet run --project Reservation.Console/Reservation.Console.csproj
dotnet run --project Reservation.Validation/Reservation.Validation.csproj
dotnet test Reservation.Tests/Reservation.Tests.csproj
```
