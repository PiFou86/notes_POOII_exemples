Result<int> succes = Result<int>.Succes(42);
Result<int> echec = Result<int>.Echec("Valeur indisponible.");

if (!succes.EstSucces || succes.Valeur != 42 || succes.MessageErreur is not null)
{
    throw new InvalidOperationException("Le résultat de succès est incohérent.");
}

if (echec.EstSucces || echec.Valeur != 0 || echec.MessageErreur != "Valeur indisponible.")
{
    throw new InvalidOperationException("Le résultat d'échec est incohérent.");
}

Console.Out.WriteLine("Validation réussie.");
