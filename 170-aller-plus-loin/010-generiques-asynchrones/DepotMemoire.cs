public sealed class DepotMemoire<T> where T : IIdentifiable
{
    private readonly List<T> m_elements = [];
    public void Ajouter(T element) => m_elements.Add(element);
    public T? TrouverParId(int id) => m_elements.SingleOrDefault(element => element.Id == id);
}
