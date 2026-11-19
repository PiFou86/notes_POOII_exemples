# CH17-E04 — Redéfinition d'opérateurs et indexeur

Source : `chapitres/170-aller-plus-loin.tex`, section `sec:operateurs-indexeurs`.
Type : console autonome.

Le scénario vérifie l'addition et la soustraction d'une `MesureMetre` (y compris
le refus d'une différence négative et l'acceptation de zéro), puis l'indexeur
validé d'une `Matrice`. Il couvre aussi les cas limites de l'exercice associé.

```bash
dotnet build OperateursIndexeurs.csproj
dotnet run --project OperateursIndexeurs.csproj
```
