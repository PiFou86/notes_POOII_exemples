# CH14-E01 — WinForms comme couche de présentation

Projet Windows seulement. `Program.cs` est la racine de composition et utilise
le Generic Host pour enregistrer le dépôt Infrastructure, l'horloge, le cas
d'utilisation Application et le formulaire. Le clic construit une commande et
affiche le DTO retourné; aucune règle métier ni persistance ne vit dans
l'événement.

```bash
dotnet build Reservation.WinForms/Reservation.WinForms.csproj
dotnet run --project Reservation.WinForms/Reservation.WinForms.csproj
```
