# Consignes pour les exemples compilables

- Les notes LaTeX du dépôt `notes_POOII_v2` constituent la source pédagogique
  principale. En cas d'ambiguïté, demander une décision avant de modifier le
  sens d'un exemple.
- Les projets portables ciblent .NET 10, compilent sans avertissement et
  respectent les normes C# du cours.
- Les exemples WinForms sont Windows seulement; ne pas présenter leur omission
  sur une autre plateforme comme une compilation réussie.
- Après une modification, valider chaque projet touché, puis lancer
  `scripts/verifier_catalogue.sh`, `scripts/compiler_tous.sh` et
  `scripts/tester_tous.sh`.
- Les scripts globaux découvrent automatiquement les projets; ne pas maintenir
  de liste manuelle de fichiers `.csproj`.
- Placer chaque type C# nommé dans son propre fichier et réserver `Program.cs`
  au point d'entrée et au scénario de démonstration.
- Ne jamais indexer les sorties de compilation, les résultats de tests ni les
  fichiers temporaires couverts par `.gitignore`.
- Lors d'un travail depuis le sous-module, commiter et pousser ici avant de
  mettre à jour le pointeur `exemples/` dans `notes_POOII_v2`.
