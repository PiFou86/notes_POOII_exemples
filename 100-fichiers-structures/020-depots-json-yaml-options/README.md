# CH10-E02 — Dépôts JSON et YAML avec configuration typée

Source : `chapitres/100-fichiers-structures.tex`, section `sec:depot-fichiers`.

Les deux dépôts respectent le contrat canonique à cinq opérations. La logique
CRUD commune est séparée de la sérialisation; JSON et YAML restent des détails
techniques. Le Generic Host charge `appsettings.json`, lie la section `depot` à
`DepotOptions`, valide les valeurs au démarrage, puis injecte
`IOptions<DepotOptions>` dans `ServiceDepotOptions`.

Cet exemple suppose un seul processus et des opérations successives. La
réécriture complète du fichier ne constitue pas une stratégie d'accès
concurrent ou transactionnel.

```bash
dotnet build DepotsFichiers.csproj
dotnet run --project DepotsFichiers.csproj
```
