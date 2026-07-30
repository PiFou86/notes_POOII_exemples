# Inventaire initial des exemples

Inventaire produit le 11 juillet 2026 à partir des notes LaTeX, des diagrammes
Mermaid et de `validation-exemples/`. Les notes contiennent 504 occurrences de
blocs `lstlisting`, d'inclusions de code ou de labels. Une occurrence n'est pas
nécessairement un exemple autonome : plusieurs fragments construisent le même
exemple et certains sont volontairement incorrects.

## Scénarios déjà validés et catalogués

| Source transitoire | Identifiant | Classement | Projet autonome |
| --- | --- | --- | --- |
| `ValidationRappelsPoo.cs` | CH01-E01 | console | oui |
| `ValidationResultGenerique.cs` | CH07-E01 | console | oui |
| `ValidationFichiersStructures.cs` | CH10-E01 | configuration et sérialisation | oui |
| `ValidationQualiteCode.cs` | CH11-E01 | console | oui |
| `ValidationStrategy.cs` | CH12-E01 | patron | oui |
| `ValidationObserver.cs` | CH12-E02 | patron | oui |
| `ValidationChainResponsabilite.cs` | CH12-E03 | patron | oui |
| `ValidationSingleton.cs` | CH12-E04 | patron | oui |
| `ValidationArchitectureBoutEnBout.cs` | CH13-E01 | architecture | oui, réécrit en cinq projets |
| `ValidationAllerPlusLoin.cs` | CH17-E01 | génériques et asynchronisme | oui |

`ValidationPatronsConception.cs` et `Program.cs` sont des orchestrateurs, pas
des exemples distincts. Les dix exemples catalogués sont maintenant autonomes.
`validation-exemples/` demeure temporairement présent pour comparer la
couverture avant toute éventuelle suppression, qui exige une autorisation.

## Ressources à poursuivre

- les blocs des chapitres 020 à 080 qui ne possèdent pas encore de scénario;
- les exemples de Repository, Factory, Builder et MVC du chapitre 120;
- les exemples WinForms, à isoler comme projets Windows;
- les extraits de revue, à classer comme contre-exemples textuels;
- les exemples des corrections, à relier à leur exercice plutôt qu'à dupliquer;
- les diagrammes Mermaid sans code, à cataloguer comme ressources et non comme
  projets artificiels.

Le découpage fin de ces 504 occurrences nécessite une lecture pédagogique par
chapitre. Lorsqu'un fragment peut raisonnablement appartenir à plusieurs
exemples, la décision doit être demandée à Pierre-François Léon.
