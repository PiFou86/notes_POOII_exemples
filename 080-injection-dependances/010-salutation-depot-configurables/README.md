# CH08-E01 — Salutation et dépôt configurables

Source : `chapitres/080-injection-dependances.tex`. Type : configuration et
console autonome.

`Host.CreateApplicationBuilder(args)` combine `appsettings.json`, les variables
d'environnement et les arguments. Le conteneur choisit une salutation et un
dépôt, puis injecte `ISalutation` dans `ServiceAccueil`. Le scénario utilise
aussi `IOptions<DepotOptions>`, vérifie qu'un service `Scoped` est partagé dans
une portée et recréé dans la suivante, puis confirme la libération des services
jetables à la fin de la portée.

```bash
dotnet build InjectionConfiguration.csproj
dotnet run --project InjectionConfiguration.csproj
depot__format=xml dotnet run --project InjectionConfiguration.csproj
dotnet run --project InjectionConfiguration.csproj -- --langue en --depot:format yaml
```
