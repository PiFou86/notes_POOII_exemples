using Microsoft.AspNetCore.Mvc;

public static class Program
{
    public static void Main(string[] args)
    {
        Guid id = Guid.NewGuid();
        IServiceEtudiants service = new ServiceEtudiantsMemoire(
            new EtudiantDto(id, "Alice Tremblay", "alice@example.test"));

        EtudiantsController controleur = new(service);
        ViewResult resultatWeb = (ViewResult)controleur.Details(id);
        EtudiantDetailsViewModel modeleWeb =
            (EtudiantDetailsViewModel)(resultatWeb.Model
                ?? throw new InvalidOperationException("Le contrôleur doit fournir un ViewModel à la vue."));
        if (modeleWeb.NomAffiche != "Alice Tremblay")
        {
            throw new InvalidOperationException("L'adaptation MVC Web est invalide.");
        }

        EtudiantEditionViewModel viewModel = new(service);
        int notifications = 0;
        viewModel.PropertyChanged += (_, _) => notifications++;
        viewModel.Charger(id);
        if (viewModel.CourrielAffiche != "alice@example.test" || notifications != 2)
        {
            throw new InvalidOperationException("Le ViewModel devrait exposer et notifier son état de présentation.");
        }

        Console.Out.WriteLine("MVC Web et MVVM validés.");
    }
}
