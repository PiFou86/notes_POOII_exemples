GestionnaireApprobation commis = new ApprobationCommis();
GestionnaireApprobation gestionnaire = new ApprobationGestionnaire();
GestionnaireApprobation direction = new ApprobationDirection();
commis.Suivant = gestionnaire;
gestionnaire.Suivant = direction;
commis.Traiter(new Demande(750m));

ITraitementChaineCaracteres majuscules = new TraitementMajuscules();
ITraitementChaineCaracteres sansDiacritiques = new TraitementSupprimerDiacritiques();
ITraitementChaineCaracteres encadrer = new TraitementPrefixeSuffixe("[", "]");
majuscules.Suivant = sansDiacritiques;
sansDiacritiques.Suivant = encadrer;
string resultat = majuscules.Traiter("Déjà");
Console.Out.WriteLine(resultat); // [DEJA]
if (resultat != "[DEJA]")
{
    throw new InvalidOperationException("Tous les maillons du pipeline devraient transformer le texte.");
}

Console.Out.WriteLine("Validation réussie.");
