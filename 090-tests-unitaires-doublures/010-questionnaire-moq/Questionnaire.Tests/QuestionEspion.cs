using ExempleQuestionnaire;

namespace ExempleQuestionnaire.Tests;

public sealed class QuestionEspion : IQuestion
{
    public int NombreCorrections { get; private set; }
    public int NombrePointsTotal => 1;
    public void PoserQuestion() { }

    public int CorrigerReponse()
    {
        NombreCorrections++;
        return 1;
    }
}
