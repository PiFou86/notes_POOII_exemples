using Microsoft.AspNetCore.Mvc;

public sealed class EtudiantsController : Controller
{
    private readonly IServiceEtudiants m_serviceEtudiants;

    public EtudiantsController(IServiceEtudiants serviceEtudiants)
    {
        ArgumentNullException.ThrowIfNull(serviceEtudiants);
        m_serviceEtudiants = serviceEtudiants;
    }

    public IActionResult Details(Guid id)
    {
        EtudiantDto etudiant = m_serviceEtudiants.Obtenir(id);
        EtudiantDetailsViewModel donnees = new(etudiant.Nom, etudiant.Courriel);
        return View(donnees);
    }
}
