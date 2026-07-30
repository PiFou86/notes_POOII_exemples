using ExempleQuestionnaire;
using FluentAssertions;
using Moq;
using Xunit;

namespace ExempleQuestionnaire.Tests;

public sealed class QuestionnaireTests
{
    [Fact]
    public void TotalPoints_ListeAvecQuestions_SommePoints()
    {
        // Arranger
        Mock<IQuestion> question1 = new();
        question1.SetupGet(question => question.NombrePointsTotal).Returns(1);
        Mock<IQuestion> question2 = new();
        question2.SetupGet(question => question.NombrePointsTotal).Returns(2);
        Mock<IQuestion> question3 = new();
        question3.SetupGet(question => question.NombrePointsTotal).Returns(4);
        Questionnaire questionnaire = new([question1.Object, question2.Object, question3.Object]);

        // Agir
        int total = questionnaire.TotalPoints;

        // Auditer
        total.Should().Be(7);
        question1.VerifyGet(question => question.NombrePointsTotal, Times.Once);
        question2.VerifyGet(question => question.NombrePointsTotal, Times.Once);
        question3.VerifyGet(question => question.NombrePointsTotal, Times.Once);
    }

    [Fact]
    public void CorrigerQuestions_QuestionsValides_SommeScore()
    {
        // Arranger
        Mock<IQuestion> question1 = new();
        question1.Setup(question => question.CorrigerReponse()).Returns(1);
        Mock<IQuestion> question2 = new();
        question2.Setup(question => question.CorrigerReponse()).Returns(2);
        Questionnaire questionnaire = new([question1.Object, question2.Object]);

        // Agir
        questionnaire.CorrigerQuestions();

        // Auditer
        questionnaire.Score.Should().Be(3);
        question1.Verify(question => question.CorrigerReponse(), Times.Once);
        question2.Verify(question => question.CorrigerReponse(), Times.Once);
        question1.VerifyNoOtherCalls();
        question2.VerifyNoOtherCalls();
    }

    [Fact]
    public void Ctor_ListeNulle_Exception()
    {
        // Arranger
        List<IQuestion>? questions = null;
        Action action = () => new Questionnaire(questions!);

        // Agir et auditer
        action.Should().Throw<ArgumentNullException>()
            .And.ParamName.Should().Be("questions");
    }

    [Fact]
    public void CorrigerQuestions_SubstitutMemoire_SommeScore()
    {
        Questionnaire questionnaire = new([new QuestionMemoire(2, 1), new QuestionMemoire(4, 3)]);
        questionnaire.CorrigerQuestions();
        questionnaire.Score.Should().Be(4);
    }
}
