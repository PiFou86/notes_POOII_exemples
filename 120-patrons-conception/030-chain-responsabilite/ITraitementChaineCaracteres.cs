public interface ITraitementChaineCaracteres
{
    ITraitementChaineCaracteres? Suivant { get; set; }
    string Traiter(string chaine);
}
