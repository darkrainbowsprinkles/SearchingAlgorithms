using System.Collections.Generic;

public class HeuristicList : IFrontier
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
            if (node.GetHeuristicValue() < bestValue)
            {
                bestValue = node.GetHeuristicValue();
                bestNode = node;
            }
        }

        list.Remove(bestNode);
        return bestNode;
    }
}