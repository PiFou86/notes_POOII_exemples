public static class Program
{
    public static void Main(string[] args)
    {
        Etudiant alice = new Etudiant("Alice");
        Etudiant autreAlice = new Etudiant("Alice");
        alice.ReussirUnCours();
        if (ReferenceEquals(alice, autreAlice) || alice.NombreCoursReussis != 1 || autreAlice.NombreCoursReussis != 0)
        {
            throw new InvalidOperationException("Les objets devraient avoir une identité et un état distincts.");
        }

        CompteBancaire compte = new CompteBancaire();
        decimal ancienSolde = compte.Solde;
        compte.Deposer(25m);
        if (compte.Solde != ancienSolde + 25m || compte.Solde < 0m)
        {
            throw new InvalidOperationException(
                "Le dépôt devrait respecter sa postcondition et préserver l'invariant du solde.");
        }

        try
        {
            compte.Deposer(-1m);
            throw new InvalidOperationException("La précondition du dépôt aurait dû refuser le montant.");
        }
        catch (ArgumentException)
        {
        }

        CarnetNotes carnet = new CarnetNotes();
        carnet.Ajouter(85);
        List<int> copie = carnet.ObtenirNotes();
        copie.Clear();
        if (carnet.ObtenirNotes().Count != 1)
        {
            throw new InvalidOperationException("La copie ne devrait pas exposer la collection interne.");
        }

        VueBase vue = new VueConnexion();
        if (vue.Afficher() != "=== Connexion ===")
        {
            throw new InvalidOperationException("L'héritage de présentation est invalide.");
        }

        Animal[] animaux = new Animal[] { new Chien(), new Chat() };
        string sons = string.Join(",", animaux.Select(animal => animal.EmettreSon()));
        if (sons != "Wouf,Miaou")
        {
            throw new InvalidOperationException("Le polymorphisme est invalide.");
        }

        Console.Out.WriteLine("Validation réussie.");
    }
}
