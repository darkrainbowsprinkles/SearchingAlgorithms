using System.Collections.Generic;

public class CostList : IFrontier
{
    List<Node> list = new();

    public bool ContainsNode(Node node)
    {
        return list.Contains(node);
    }

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
        int bestCost = int.MaxValue;
        Node bestNode = null;

        foreach (Node node in list)
        {
            int pathCost = node.GetPathCost();
            int heuristicValue = node.GetHeuristicValue();
            int totalCost = pathCost + heuristicValue;

            if (totalCost < bestCost)
            {
                bestCost = totalCost;
                bestNode = node;
            }
        }

        list.Remove(bestNode);
        return bestNode;
    }
}