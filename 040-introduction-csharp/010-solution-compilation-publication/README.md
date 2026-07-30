# CH04-E01 — Structure, compilation, exécution et publication

La solution `ExempleSolution.slnx` regroupe une bibliothèque et une console.
La console rend visible la référence de projet vers la bibliothèque.

```bash
dotnet sln ExempleSolution.slnx list
dotnet build ExempleSolution.slnx
dotnet build ApplicationConsole/ApplicationConsole.csproj
dotnet run --project ApplicationConsole/ApplicationConsole.csproj
dotnet publish ApplicationConsole/ApplicationConsole.csproj -c Release
```

La publication produit une application déployable dans `bin/Release/net10.0/publish`.
