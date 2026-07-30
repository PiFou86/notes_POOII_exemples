public abstract class VueBase
{
    public string Titre { get; }
    protected VueBase(string titre) => Titre = titre;
    public abstract string Afficher();
}
