using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] int size = 9;
    Dictionary<Vector2Int, Node> nodes = new();
    HashSet<Node> visited = new();
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left
    };

    public IEnumerable<Node> GetNodes()
    {
        return nodes.Values;
    }

    public List<Node> StartSearch(SearchType searchType)
    {
        ClearGraph();

        switch (searchType)
        {
            case SearchType.BFS:
                return Search(new QueueFrontier());

            case SearchType.DFS:
                return Search(new StackFrontier());
        }

        return null;
    }

    void Start()
    {
        CreateGraph();
    }

    void ClearGraph()
    {
        foreach (Node node in nodes.Values)
        {
            node.SetParent(null);
            node.SetIsExplored(false);
            node.SetIsPath(false);
        }
    }

    List<Node> Search(IFrontier frontier)
    {
        Node startNode = GetNode(new Vector2Int(0, 0));
        Node goalNode = GetNode(new Vector2Int(8, 8));

        visited.Clear();

        frontier.Add(startNode);
        visited.Add(startNode);

        while (frontier.GetSize() != 0)
        {
            Node currentNode = frontier.Take();

            if (goalNode == currentNode)
            {
                break;
            }

            VisitNode(frontier, currentNode);
        }

        return GetPath(goalNode);
    }

    void VisitNode(IFrontier frontier, Node currentNode)
    {
        visited.Add(currentNode);
        currentNode.SetIsExplored(true);

        foreach (Node neighbour in GetNeighbours(currentNode))
        {
            if (visited.Contains(neighbour))
            {
                continue;
            }

            neighbour.SetParent(currentNode);
            visited.Add(neighbour);
            frontier.Add(neighbour);
        }
    }

    List<Node> GetPath(Node goalNode)
    {
        List<Node> path = new();
        Node currentNode = goalNode;

        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode.SetIsPath(true);
            currentNode = currentNode.GetParent();
        }

        path.Reverse();
        return path;
    }
    
    Node GetNode(Vector2Int coordinates)
    {
        if (!nodes.ContainsKey(coordinates))
        {
            return null;
        }

        return nodes[coordinates];
    }

    IEnumerable<Node> GetNeighbours(Node node)
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourDirection = node.GetCoordinates() + direction;

            if (nodes.ContainsKey(neighbourDirection))
            {
                yield return nodes[neighbourDirection];
            }
        }
    }

    void CreateGraph()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                CreateNode(x, y);
            }
        }
    }

    void CreateNode(int x, int y)
    {
        Vector2Int coordinates = new(x, y);
        Node newNode = new(coordinates);
        nodes[coordinates] = newNode;
    }
}
