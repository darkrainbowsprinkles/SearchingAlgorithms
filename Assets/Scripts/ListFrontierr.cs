using System.Collections.Generic;

public class ListFrontier : IFrontier
{
    List<Node> list = new();

    public int GetSize()
    {
        return list.Count;
    }
    
    public void Add(Node node)
    {
        list.Add(node);
    }

    public Node Take()
    {
        int bestValue = int.MaxValue;
        Node bestNode = null;

        foreach (Node node in list)
        {
            if (node.GetValue() < bestValue)
            {
                bestValue = node.GetValue();
                bestNode = node;
            }
        }

        list.Remove(bestNode);
        return bestNode;
    }
}