# CH09-E01 — Questionnaire testé avec xUnit, Moq et FluentAssertions

Source : `chapitres/090-tests-unitaires-doublures.tex`. Type : bibliothèque et
projet de tests.

Les tests reprennent la structure AAA et l'exemple canonique `IQuestion` /
`Questionnaire`. `SetupGet` et `Setup` fournissent des réponses contrôlées;
`Verify`, `VerifyGet`, `Times.Once` et `VerifyNoOtherCalls` vérifient les
interactions. Les classes `QuestionFantome`, `QuestionBouchon`,
`QuestionMemoire` et `QuestionEspion` rendent les quatre autres rôles visibles;
Moq sert de simulacre lorsque l'interaction est l'attente du test.

```bash
dotnet build Questionnaire/Questionnaire.csproj
dotnet test Questionnaire.Tests/Questionnaire.Tests.csproj
```
