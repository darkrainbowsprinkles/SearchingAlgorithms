public interface IFrontier
{
    int GetSize();
    void Add(Node node);
    Node Take();
}