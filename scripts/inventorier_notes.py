#!/usr/bin/env python3
import pathlib
import re
import hashlib

DEPOT = pathlib.Path(__file__).resolve().parents[2]
CIBLE = DEPOT / "exemples" / "INVENTAIRE_BLOCS_CODE.md"


def type_bloc(options, contenu):
    texte = (options + "\n" + contenu).lower()
    if any(mot in texte for mot in ("ne compile pas", "incorrect", "violation", "problème")):
        return "contre-exemple potentiel"
    if any(mot in texte for mot in ("appsettings", "json", "yaml", "xml", "csv")):
        return "configuration ou données"
    if any(mot in texte for mot in ("assert", "[fact]", "mock<", "fluentassertions")):
        return "test"
    if "class " in texte or "interface " in texte or "record " in texte:
        return "code C#"
    return "fragment"


def blocs(fichier):
    lignes = fichier.read_text(encoding="utf-8").splitlines()
    resultat = []
    debut = re.compile(r"\\begin\{lstlisting\}(?:\[(.*)\])?")
    section = "Introduction"
    for index, ligne in enumerate(lignes):
        titre = re.search(r"\\(?:section|subsection|subsubsection)\{([^}]*)\}", ligne)
        if titre:
            section = titre.group(1)
        trouve = debut.search(ligne)
        if not trouve:
            continue
        fin = index + 1
        while fin < len(lignes) and "\\end{lstlisting}" not in lignes[fin]:
            fin += 1
        contenu = "\n".join(lignes[index + 1:fin])
        options = trouve.group(1) or ""
        normalise = re.sub(r"\s+", " ", contenu).strip()
        empreinte = hashlib.sha256(normalise.encode("utf-8")).hexdigest()[:12]
        resultat.append((index + 1, fin + 1, options, type_bloc(options, contenu), section, empreinte))
    return resultat


def main():
    fichiers = sorted((DEPOT / "chapitres").glob("*.tex")) + sorted((DEPOT / "annexes").glob("*.tex"))
    inventaire = [(fichier, blocs(fichier)) for fichier in fichiers]
    total = sum(len(elements) for _, elements in inventaire)
    diagrammes = sorted((DEPOT / "uml" / "classDiagrams").glob("**/*.mmd"))
    ressources = []
    for extension in ("*.json", "*.yaml", "*.yml", "*.xml", "*.csv"):
        ressources.extend(DEPOT.glob(f"**/{extension}"))
    ressources = sorted(p for p in ressources if "bin" not in p.parts and "obj" not in p.parts)

    lignes = [
        "# Inventaire mécanique des blocs de code",
        "",
        "Ce fichier est généré depuis les notes. Le classement est heuristique et doit",
        "être confirmé pédagogiquement avant extraction d'un nouvel exemple.",
        "",
        f"- blocs `lstlisting` : {total};",
        f"- diagrammes Mermaid : {len(diagrammes)};",
        f"- ressources JSON, YAML, XML ou CSV : {len(ressources)}.",
        "",
        "## Blocs par fichier",
        "",
        "| Fichier | Section | Lignes | Options | Classement initial | Empreinte |",
        "| --- | --- | ---: | --- | --- | --- |",
    ]
    for fichier, elements in inventaire:
        relatif = fichier.relative_to(DEPOT).as_posix()
        for debut, fin, options, classement, section, empreinte in elements:
            propres = options.replace("|", "\\|") or "—"
            lignes.append(
                f"| `{relatif}` | {section} | {debut}–{fin} | `{propres}` | "
                f"{classement} | `{empreinte}` |"
            )
    repetitions = {}
    for fichier, elements in inventaire:
        for debut, fin, options, classement, section, empreinte in elements:
            repetitions.setdefault(empreinte, []).append(
                f"{fichier.relative_to(DEPOT).as_posix()}:{debut}"
            )
    lignes.extend(["", "## Répétitions textuelles exactes", ""])
    doublons = {cle: valeurs for cle, valeurs in repetitions.items() if len(valeurs) > 1}
    if doublons:
        for empreinte, emplacements in sorted(doublons.items()):
            lignes.append(f"- `{empreinte}` : " + ", ".join(f"`{e}`" for e in emplacements))
    else:
        lignes.append("Aucune répétition textuelle exacte détectée.")
    lignes.extend(["", "## Ressources structurées", ""])
    if ressources:
        lignes.extend(f"- `{p.relative_to(DEPOT).as_posix()}`" for p in ressources)
    else:
        lignes.append("Aucune ressource autonome trouvée.")
    lignes.extend(["", "## Diagrammes Mermaid", ""])
    lignes.extend(f"- `{p.relative_to(DEPOT).as_posix()}`" for p in diagrammes)
    lignes.append("")
    nouveau = "\n".join(lignes)
    if not CIBLE.exists() or CIBLE.read_text(encoding="utf-8") != nouveau:
        CIBLE.write_text(nouveau, encoding="utf-8")


if __name__ == "__main__":
    main()
