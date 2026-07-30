using System.Linq.Expressions;

Expression<Func<int, bool>> estPair = nombre =>
    nombre % 2 == 0;

Console.Out.WriteLine($"Expression : {estPair}");
Console.Out.WriteLine("Structure :");
AfficheurExpression.AfficherStructure(estPair);
