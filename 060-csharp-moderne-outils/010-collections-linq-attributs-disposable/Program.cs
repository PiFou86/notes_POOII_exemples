using System.ComponentModel.DataAnnotations;

List<Produit> produits = new List<Produit>
{
    new Produit("Café", 4m),
    new Produit("Repas", 18m)
};
HashSet<string> categories = new HashSet<string>
{
    "Boisson",
    "Repas",
    "Boisson"
};
Dictionary<string, Produit> parNom = produits.ToDictionary(produit => produit.Nom);
Queue<Produit> file = new Queue<Produit>(produits);
Stack<Produit> pile = new Stack<Produit>(produits);
if (categories.Count != 2 || parNom["Café"].Prix != 4m || file.Dequeue().Nom != "Café" || pile.Pop().Nom != "Repas")
{
    throw new InvalidOperationException("Une collection ne respecte pas son rôle.");
}

Func<Produit, bool> estDispendieux = produit => produit.Prix >= 10m;
List<string> noms = produits.Where(estDispendieux).Select(produit => produit.Nom).ToList();
Action<string> afficher = message => Console.Out.WriteLine(message);
afficher(string.Join(", ", noms));

IEnumerable<Produit> requeteDifferee = produits.Where(produit => produit.Prix >= 5m);
produits.Add(new Produit("Dessert", 7m));
if (requeteDifferee.Count() != 2)
{
    throw new InvalidOperationException("La requête LINQ devrait être exécutée au parcours.");
}

ProduitSaisi produitInvalide = new ProduitSaisi();
List<ValidationResult> erreurs = new List<ValidationResult>();
ValidationContext contexte = new ValidationContext(produitInvalide);
bool estValide = Validator.TryValidateObject(
    produitInvalide,
    contexte,
    erreurs,
    validateAllProperties: true);
if (estValide || erreurs.Count == 0)
{
    throw new InvalidOperationException("La validation devrait détecter les données invalides.");
}

Produit? produitUnique = produits.SingleOrDefault(produit => produit.Nom == "Café");
List<Produit> premierePage = produits
    .OrderBy(produit => produit.Nom)
    .Skip(0)
    .Take(2)
    .ToList();
if (produitUnique == null || premierePage.Count != 2)
{
    throw new InvalidOperationException("La recherche ou la pagination LINQ est invalide.");
}

RessourceTemporaire ressource = new RessourceTemporaire();
using (ressource)
{
    afficher("Ressource utilisée.");
}
if (!ressource.EstLiberee)
{
    throw new InvalidOperationException("La ressource devrait être libérée.");
}

Console.Out.WriteLine("Validation réussie.");
