public static class Program
{
    public static void Main(string[] args)
    {
        string? dernierMessage = null;
        Cours cours = new();
        cours.NoteAjoutee += message => dernierMessage = message;
        cours.AjouterNote(85);
        if (dernierMessage != "Note ajoutée: 85")
        {
            throw new InvalidOperationException("L'événement n'a pas été publié.");
        }

        CoursObservable observable = new();
        ObservateurValidation observateur = new();
        using (IDisposable abonnement = observable.Subscribe(observateur))
        {
            observable.AjouterNote(90);
        }
        observable.AjouterNote(95);
        if (observateur.NotesRecues.Count != 1 || observateur.NotesRecues[0] != 90)
        {
            throw new InvalidOperationException("Le désabonnement est invalide.");
        }

        ObservateurConsole observateurConsole = new();
        using (IDisposable abonnement = observable.Subscribe(observateurConsole))
        {
            observable.AjouterNote(100);
            observable.TerminerObservation();
        }

        if (!observateurConsole.EstTermine)
        {
            throw new InvalidOperationException("L'observateur devrait recevoir la fin du flux.");
        }

        CoursObservable observableAction = new();
        List<int> notesAction = [];
        using (ObservateurAction<NoteEvenement> observateurAction = new(
            observableAction,
            evenement => notesAction.Add(evenement.Note)))
        {
            observableAction.AjouterNote(70);
        }
        observableAction.AjouterNote(75);
        if (notesAction.Count != 1 || notesAction[0] != 70)
        {
            throw new InvalidOperationException("L'observateur à action devrait se désabonner.");
        }

        Exception erreurAttendue = new InvalidOperationException("Erreur simulée.");
        Exception? erreurRecue = null;
        CoursObservable observableErreur = new();
        using ObservateurAction<NoteEvenement> observateurErreur = new(
            observableErreur,
            evenement => notesAction.Add(evenement.Note),
            erreur => erreurRecue = erreur);
        observableErreur.SignalerErreur(erreurAttendue);
        if (!ReferenceEquals(erreurRecue, erreurAttendue))
        {
            throw new InvalidOperationException("L'observateur à action devrait recevoir l'erreur.");
        }

        bool estTermine = false;
        CoursObservable observableTermine = new();
        using ObservateurAction<NoteEvenement> observateurTermine = new(
            observableTermine,
            evenement => notesAction.Add(evenement.Note),
            terminer: () => estTermine = true);
        observableTermine.TerminerObservation();
        if (!estTermine)
        {
            throw new InvalidOperationException("L'observateur à action devrait recevoir la fin du flux.");
        }

        Console.Out.WriteLine("Validation réussie.");
    }
}
