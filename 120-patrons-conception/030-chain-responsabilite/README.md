# CH12-E03 — Deux variantes de la chaîne de responsabilité

L'exemple compare les deux règles de propagation présentées dans les notes. Une
demande traverse des gestionnaires jusqu'au premier qui peut l'approuver. Un
texte traverse ensuite tous les maillons d'un pipeline et devient `[DEJA]`.

```bash
dotnet run --project ChainResponsabilite.csproj
```
