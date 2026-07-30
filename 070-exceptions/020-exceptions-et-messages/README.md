# CH07-E02 — Exceptions personnalisées, causes et messages sécuritaires

Source : `chapitres/070-exceptions.tex`, section `sec:csharp-exceptions`.

Le scénario distingue une anomalie métier exceptionnelle d'une erreur
technique. Il enveloppe une `JsonException` dans une exception d'importation en
conservant la cause dans `InnerException`. La présentation reçoit un message
stable, tandis que le journal technique conserve l'exception sans l'exposer à
l'utilisateur.

```bash
dotnet build ExceptionsMessages.csproj
dotnet run --project ExceptionsMessages.csproj
```
