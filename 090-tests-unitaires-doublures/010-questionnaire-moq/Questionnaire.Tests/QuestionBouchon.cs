using ExempleQuestionnaire;

namespace ExempleQuestionnaire.Tests;

public sealed class QuestionBouchon : IQuestion
{
    private readonly int m_score;
    public int NombrePointsTotal { get; }

    public QuestionBouchon(int nombrePointsTotal, int score)
    {
        NombrePointsTotal = nombrePointsTotal;
        m_score = score;
    }

    public void PoserQuestion() { }
    public int CorrigerReponse() => m_score;
}
