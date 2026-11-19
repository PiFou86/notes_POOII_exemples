using System.Linq.Expressions;

public static class Program
{
    public static void Main(string[] args)
    {
        Expression<Func<int, bool>> estPair = nombre =>
            nombre % 2 == 0;

        Console.Out.WriteLine($"Expression : {estPair}");
        Console.Out.WriteLine("Structure :");
        AfficheurExpression.AfficherStructure(estPair);
    }
}
