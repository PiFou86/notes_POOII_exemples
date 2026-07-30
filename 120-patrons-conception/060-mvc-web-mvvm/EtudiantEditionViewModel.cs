using System.ComponentModel;
using System.Runtime.CompilerServices;

public sealed class EtudiantEditionViewModel : INotifyPropertyChanged
{
    private readonly IServiceEtudiants m_serviceEtudiants;
    private string m_nomAffiche = "";
    private string m_courrielAffiche = "";

    public event PropertyChangedEventHandler? PropertyChanged;

    public string NomAffiche
    {
        get => m_nomAffiche;
        private set
        {
            m_nomAffiche = value;
            NotifierChangement();
        }
    }

    public string CourrielAffiche
    {
        get => m_courrielAffiche;
        private set
        {
            m_courrielAffiche = value;
            NotifierChangement();
        }
    }

    public EtudiantEditionViewModel(IServiceEtudiants serviceEtudiants)
    {
        ArgumentNullException.ThrowIfNull(serviceEtudiants);
        m_serviceEtudiants = serviceEtudiants;
    }

    public void Charger(Guid id)
    {
        EtudiantDto etudiant = m_serviceEtudiants.Obtenir(id);
        NomAffiche = etudiant.Nom;
        CourrielAffiche = etudiant.Courriel;
    }

    private void NotifierChangement([CallerMemberName] string? nomPropriete = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
    }
}
