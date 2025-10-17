using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] int size = 9;
    Dictionary<Vector2Int, Node> nodes = new();
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

    public void StartSearch()
    {
        BreadthFirstSearch();
    }

    void Start()
    {
        CreateGraph();
    }
    
    List<Node> BreadthFirstSearch()
    {
        Node startNode = GetNode(new Vector2Int(0, 0));
        Node goalNode = GetNode(new Vector2Int(8, 8));

        Queue<Node> frontier = new();
        HashSet<Node> visited = new();

        frontier.Enqueue(startNode);

        while (frontier.Count != 0)
        {
            Node currentNode = frontier.Dequeue();

            if (goalNode == currentNode)
            {
                break;
            }

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
                frontier.Enqueue(neighbour);
            }
        }

        return GetPath(goalNode);
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
