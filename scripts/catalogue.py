#!/usr/bin/env python3
import json
import pathlib
import re
import sys

RACINE = pathlib.Path(__file__).resolve().parents[1]
DEPOT = RACINE.parent


def manifestes():
    return sorted(RACINE.glob("**/exemple.json"))


def charger():
    resultat = []
    for chemin in manifestes():
        with chemin.open(encoding="utf-8") as fichier:
            donnees = json.load(fichier)
        donnees["_chemin"] = chemin
        resultat.append(donnees)
    return resultat


def index(contenus):
    lignes = [
        "# Index des exemples",
        "",
        "Ce fichier est généré depuis les manifestes `exemple.json`.",
        "",
        "| Identifiant | Chapitre | Titre | Type | Notions | Exécutable |",
        "| --- | --- | --- | --- | --- | --- |",
    ]
    for item in sorted(contenus, key=lambda x: x["id"]):
        notions = ", ".join(item.get("notions", []))
        executable = item.get("projetExecutable", "—")
        dossier = item["_chemin"].parent.relative_to(RACINE)
        titre = f'[{item["titre"]}]({dossier.as_posix()}/README.md)'
        lignes.append(
            f'| {item["id"]} | {item["chapitre"]} | {titre} | '
            f'{item["type"]} | {notions} | {executable} |'
        )
    lignes.append("")
    return "\n".join(lignes)


def catalogue(contenus):
    public = []
    for item in sorted(contenus, key=lambda x: x["id"]):
        public.append({cle: valeur for cle, valeur in item.items() if cle != "_chemin"})
    return json.dumps(public, ensure_ascii=False, indent=2) + "\n"


def readme_chapitre(chapitre, contenus):
    lignes = [f"# Exemples — {chapitre}", ""]
    elements = [item for item in contenus if item["chapitre"] == chapitre]
    if not elements:
        lignes.append("Aucun exemple autonome n'a encore été extrait pour ce chapitre.")
    else:
        for item in sorted(elements, key=lambda x: x["id"]):
            dossier = item["_chemin"].parent.name
            lignes.append(f'- [{item["id"]} — {item["titre"]}]({dossier}/README.md)')
    lignes.append("")
    return "\n".join(lignes)


def verifier(contenus):
    erreurs = []
    obligatoires = {"id", "titre", "chapitre", "sectionLatex", "type", "projets", "portable", "statut"}
    ids = [item.get("id") for item in contenus]
    for identifiant in sorted(set(ids)):
        if ids.count(identifiant) > 1:
            erreurs.append(f"identifiant dupliqué : {identifiant}")
    projets_declares = {}
    notes = "\n".join(p.read_text(encoding="utf-8") for p in sorted((DEPOT / "chapitres").glob("*.tex")))
    for item in contenus:
        chemin = item["_chemin"]
        manquants = obligatoires - item.keys()
        if manquants:
            erreurs.append(f"{chemin}: champs manquants {sorted(manquants)}")
        label = item.get("sectionLatex", "")
        if label and f"\\label{{{label}}}" not in notes:
            erreurs.append(f"{chemin}: label LaTeX absent : {label}")
        for relatif in item.get("projets", []):
            projet = (chemin.parent / relatif).resolve()
            if not projet.is_file():
                erreurs.append(f"{chemin}: projet absent : {relatif}")
            projets_declares.setdefault(projet, []).append(item.get("id"))
        executable = item.get("projetExecutable")
        if executable and not (chemin.parent / executable).is_file():
            erreurs.append(f"{chemin}: exécutable absent : {executable}")
    projets_reels = set(RACINE.glob("**/*.csproj"))
    for projet in sorted(projets_reels):
        declarations = projets_declares.get(projet.resolve(), [])
        if len(declarations) != 1:
            erreurs.append(f"{projet}: {len(declarations)} déclaration(s) dans les manifestes")
    declaration = re.compile(
        r"^public\s+(?:(?:sealed|abstract|static|readonly|partial)\s+)*"
        r"(?:class|interface|record|struct)\s+([A-Za-z_][A-Za-z0-9_]*)",
        re.MULTILINE,
    )
    for fichier_cs in sorted(RACINE.glob("**/*.cs")):
        if "bin" in fichier_cs.parts or "obj" in fichier_cs.parts:
            continue
        contenu = fichier_cs.read_text(encoding="utf-8")
        types = declaration.findall(contenu)
        if len(types) > 1:
            erreurs.append(f"{fichier_cs}: plusieurs types publics nommés : {types}")
        if fichier_cs.name == "Program.cs":
            if types and types != ["Program"]:
                erreurs.append(f"{fichier_cs}: Program.cs contient un autre type public : {types}")
            if not re.search(r"\bclass\s+Program\b", contenu) or not re.search(
                r"\bstatic\s+(?:async\s+)?(?:void|int|Task(?:<int>)?)\s+Main\s*\(",
                contenu,
            ):
                erreurs.append(f"{fichier_cs}: une classe Program et une méthode Main explicites sont requises")
        if len(types) == 1 and fichier_cs.stem != types[0]:
            erreurs.append(f"{fichier_cs}: le fichier devrait porter le nom {types[0]}.cs")
    attendu = index(contenus)
    index_path = RACINE / "INDEX_EXEMPLES.md"
    if not index_path.exists() or index_path.read_text(encoding="utf-8") != attendu:
        erreurs.append("INDEX_EXEMPLES.md est absent ou périmé")
    catalogue_path = RACINE / "catalogue.json"
    if not catalogue_path.exists() or catalogue_path.read_text(encoding="utf-8") != catalogue(contenus):
        erreurs.append("catalogue.json est absent ou périmé")
    for dossier in sorted(p for p in RACINE.iterdir() if p.is_dir() and re.match(r"^[0-9]{3}-", p.name)):
        attendu_readme = readme_chapitre(dossier.name, contenus)
        readme = dossier / "README.md"
        if not readme.exists() or readme.read_text(encoding="utf-8") != attendu_readme:
            erreurs.append(f"{readme}: absent ou périmé")
    return erreurs


def main():
    contenus = charger()
    action = sys.argv[1] if len(sys.argv) > 1 else "verifier"
    if action == "index":
        cible = RACINE / "INDEX_EXEMPLES.md"
        nouveau = index(contenus)
        if not cible.exists() or cible.read_text(encoding="utf-8") != nouveau:
            cible.write_text(nouveau, encoding="utf-8")
        catalogue_path = RACINE / "catalogue.json"
        nouveau_catalogue = catalogue(contenus)
        if not catalogue_path.exists() or catalogue_path.read_text(encoding="utf-8") != nouveau_catalogue:
            catalogue_path.write_text(nouveau_catalogue, encoding="utf-8")
        for dossier in sorted(p for p in RACINE.iterdir() if p.is_dir() and re.match(r"^[0-9]{3}-", p.name)):
            readme = dossier / "README.md"
            nouveau_readme = readme_chapitre(dossier.name, contenus)
            if not readme.exists() or readme.read_text(encoding="utf-8") != nouveau_readme:
                readme.write_text(nouveau_readme, encoding="utf-8")
        return
    erreurs = verifier(contenus)
    if erreurs:
        print("\n".join(f"ERREUR : {erreur}" for erreur in erreurs), file=sys.stderr)
        raise SystemExit(1)
    print(f"Catalogue valide : {len(contenus)} exemple(s).")


if __name__ == "__main__":
    main()
