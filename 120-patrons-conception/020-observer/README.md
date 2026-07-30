# CH12-E02 — Observer avec `event` et `IObservable<T>`

Compare l'événement C# et les interfaces standard. Le code client possède les
abonnements et les libère explicitement avec `using`. Les observateurs se
concentrent sur le traitement des valeurs, des erreurs et de la fin du flux.

L'exemple contient aussi `ObservateurAction<T>`, une variante qui reçoit le
sujet et des callbacks dans son constructeur. Cette variante possède son
abonnement et doit elle-même être libérée avec `Dispose`.

```bash
dotnet run --project Observer.csproj
```
