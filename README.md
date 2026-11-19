# Exemples compilables

Ce répertoire regroupe les exemples autonomes tirés des notes de cours. Les
notes LaTeX demeurent la source principale; les projets servent à vérifier que
le code présenté compile et produit le comportement annoncé.

Les programmes déclarent explicitement leur classe `Program` et leur méthode
`Main`. Les exemples asynchrones emploient `async Task Main(string[] args)`.
Le fichier `Program.cs` contient le point d'entrée et le scénario de
démonstration; les autres types nommés ont chacun leur fichier.

## Trouver un exemple

- consulter [l'index](INDEX_EXEMPLES.md);
- chercher un identifiant : `rg CH12-E01 exemples/`;
- chercher une notion : `rg -i 'singleton|architecture|result' exemples/`.

## Commandes

Depuis la racine de ce dépôt autonome :

```bash
./scripts/verifier_catalogue.sh
./scripts/compiler_tous.sh
./scripts/tester_tous.sh
./scripts/executer_exemple.sh CH12-E01
./scripts/generer_index.sh
```

Lorsque ce dépôt est utilisé comme sous-module de `notes_POOII_v2`, préfixer
ces chemins par `exemples/` depuis la racine du dépôt parent.

Les projets sont découverts automatiquement. Les exemples WinForms sont
signalés comme non portables et ignorés hors de Windows.

## Contribuer depuis le dépôt des notes

Une modification doit d'abord être validée, commitée et poussée dans ce dépôt.
Le dépôt `notes_POOII_v2` doit ensuite enregistrer le nouveau commit du
sous-module `exemples/`.
