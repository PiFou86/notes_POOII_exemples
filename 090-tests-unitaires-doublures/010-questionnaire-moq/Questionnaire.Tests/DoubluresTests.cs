using ExempleQuestionnaire;
using FluentAssertions;
using Moq;
using Xunit;

namespace ExempleQuestionnaire.Tests;

public sealed class DoubluresTests
{
    [Fact]
    public void Score_QuestionnaireNonDemarre_FantomeNonUtilise()
    {
        Questionnaire questionnaire = new([new QuestionFantome()]);
        questionnaire.Score.Should().Be(0);
    }

    [Fact]
    public void CorrigerQuestions_Bouchon_RetourControle()
    {
        Questionnaire questionnaire = new([new QuestionBouchon(5, 3)]);
        questionnaire.CorrigerQuestions();
        questionnaire.Score.Should().Be(3);
    }

    [Fact]
    public void CorrigerQuestions_Espion_EnregistreAppel()
    {
        QuestionEspion espion = new();
        Questionnaire questionnaire = new([espion]);
        questionnaire.CorrigerQuestions();
        espion.NombreCorrections.Should().Be(1);
    }

    [Fact]
    public void CorrigerQuestions_Simulacre_VerifieInteraction()
    {
        Mock<IQuestion> simulacre = new();
        simulacre.Setup(question => question.CorrigerReponse()).Returns(1);
        Questionnaire questionnaire = new([simulacre.Object]);
        questionnaire.CorrigerQuestions();
        simulacre.Verify(question => question.CorrigerReponse(), Times.Once);
        simulacre.VerifyNoOtherCalls();
    }
}
