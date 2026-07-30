using System.Linq.Expressions;

public static class AfficheurExpression
{
    public static void AfficherStructure(Expression<Func<int, bool>> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        VisiteurStructure visiteur = new();
        visiteur.Visit(expression);
    }

    private sealed class VisiteurStructure : ExpressionVisitor
    {
        private int m_niveau;

        public override Expression? Visit(Expression? noeud)
        {
            if (noeud is null)
            {
                return null;
            }

            string indentation = new(' ', m_niveau * 2);
            Console.Out.WriteLine(
                $"{indentation}{noeud.NodeType} : {noeud.Type.Name}{ObtenirDetails(noeud)}");

            m_niveau++;

            try
            {
                return base.Visit(noeud);
            }
            finally
            {
                m_niveau--;
            }
        }

        private static string ObtenirDetails(Expression noeud)
        {
            return noeud switch
            {
                LambdaExpression lambda =>
                    $" — paramètres : {string.Join(", ", lambda.Parameters.Select(p => p.Name))}",
                ParameterExpression parametre => $" — nom : {parametre.Name}",
                ConstantExpression constante => $" — valeur : {constante.Value ?? "null"}",
                MemberExpression membre => $" — membre : {membre.Member.Name}",
                MethodCallExpression appel => $" — méthode : {appel.Method.Name}",
                BinaryExpression binaire => $" — opérateur : {binaire.NodeType}",
                UnaryExpression unaire => $" — opérateur : {unaire.NodeType}",
                _ => string.Empty
            };
        }
    }
}
