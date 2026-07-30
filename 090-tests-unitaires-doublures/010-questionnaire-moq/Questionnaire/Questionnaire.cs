namespace ExempleQuestionnaire;

public sealed class Questionnaire
{
    private readonly List<IQuestion> m_questions;
    public int Score { get; private set; }
    public int TotalPoints => m_questions.Sum(question => question.NombrePointsTotal);

    public Questionnaire(List<IQuestion> questions)
    {
        ArgumentNullException.ThrowIfNull(questions);
        m_questions = questions;
    }

    public void CorrigerQuestions()
    {
        Score = m_questions.Sum(question => question.CorrigerReponse());
    }
}
