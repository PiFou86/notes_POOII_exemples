# CH11-E02 — CQS, SOLID et métriques

Source : `chapitres/110-principes-conception-qualite.tex`, notamment
`sec:metriques-ca-ce-i`. Le scénario sépare commandes et requêtes, utilise une
abstraction honnête pour carré/rectangle et calcule `Ca`, `Ce` et `I`.
Quand `Ca + Ce` vaut zéro, `I` est indéfinie; l'exemple représente donc ce cas
par une valeur nullable plutôt que par un zéro trompeur.

STUPID est traité comme une liste de risques à repérer : état global du
Singleton, couplage concret, code intestable, optimisation prématurée, nommage
peu descriptif et duplication. Ces défauts ne sont pas intégrés au projet
compilé comme modèles à reproduire.

```bash
dotnet run --project QualiteConception.csproj
```
