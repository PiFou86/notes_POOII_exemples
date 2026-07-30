namespace ExempleQuestionnaire;

public interface IQuestion
{
    int NombrePointsTotal { get; }
    void PoserQuestion();
    int CorrigerReponse();
}
