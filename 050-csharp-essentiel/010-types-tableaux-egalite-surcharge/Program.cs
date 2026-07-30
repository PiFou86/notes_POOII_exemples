int valeur = 10;
int copieValeur = valeur;
copieValeur++;
if (valeur != 10)
{
    throw new InvalidOperationException("La copie d'une valeur ne devrait pas modifier l'original.");
}

Cours cours = new Cours(" 420-W30-SF ", " Programmation orientée objet II ");
Cours copieReference = cours;
if (!ReferenceEquals(cours, copieReference))
{
    throw new InvalidOperationException("Les variables devraient référencer le même objet.");
}

Cours coursEquivalent = new Cours("420-w30-sf", "Programmation orientée objet II");
if (!cours.Equals(coursEquivalent) || cours.GetHashCode() != coursEquivalent.GetHashCode())
{
    throw new InvalidOperationException("Le contrat d'égalité est incohérent.");
}

string[] noms = new string[] { "Alice", "Bob", "Chloé", "David" };
string dernier = noms[^1];
string[] milieu = noms[1..3];
if (dernier != "David" || milieu.Length != 2)
{
    throw new InvalidOperationException("Les index ou plages sont incorrects.");
}

Calculatrice calculatrice = new Calculatrice();
if (calculatrice.Additionner(2, 3) != 5
    || calculatrice.Additionner(2.5f, 3.5f) != 6f
    || calculatrice.Additionner(1, 2, 3) != 6)
{
    throw new InvalidOperationException("Une surcharge a produit un mauvais résultat.");
}

string? saisie = null;
string nomAffiche = saisie ?? "Anonyme";
string categorie = valeur >= 10 ? "dix ou plus" : "moins de dix";
if (nomAffiche != "Anonyme" || categorie != "dix ou plus")
{
    throw new InvalidOperationException("Les expressions conditionnelles sont incorrectes.");
}

MesureMetre longueur = new MesureMetre(2);
MesureMetre largeur = new MesureMetre(2);
MesureMetre difference = longueur - largeur;
if (difference.Valeur != 0)
{
    throw new InvalidOperationException("Une différence nulle devrait être valide.");
}

Matrice matrice = new Matrice(2, 3);
matrice[1, 2] = 7.5;
if (matrice[1, 2] != 7.5)
{
    throw new InvalidOperationException("L'indexeur devrait retourner la valeur assignée.");
}

bool indexInvalideRefuse = false;
try
{
    double valeurInvalide = matrice[0, 3];
}
catch (ArgumentOutOfRangeException)
{
    indexInvalideRefuse = true;
}

if (!indexInvalideRefuse)
{
    throw new InvalidOperationException("L'indexeur devrait valider les index.");
}

Console.Out.WriteLine(cours);
Console.Out.WriteLine("Validation réussie.");
