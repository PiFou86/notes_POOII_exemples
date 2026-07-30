using ExempleQuestionnaire;

namespace ExempleQuestionnaire.Tests;

public sealed class QuestionFantome : IQuestion
{
    public int NombrePointsTotal => throw new NotSupportedException();
    public void PoserQuestion() => throw new NotSupportedException();
    public int CorrigerReponse() => throw new NotSupportedException();
}
