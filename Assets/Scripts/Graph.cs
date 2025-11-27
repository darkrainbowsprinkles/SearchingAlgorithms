using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] int size = 9;
    Dictionary<Vector2Int, Node> nodes = new();
    HashSet<Node> visited = new();
    IFrontier frontier;
    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left
    };
    Node startNode;
    Node goalNode;

    public IEnumerable<Node> GetVisited()
    {
        return visited;
    }

    public IEnumerable<Node> GetNodes()
    {
        return nodes.Values;
    }

    public void SetStartNode(Node node)
    {
        if (node == startNode)
        {
            return;
        }

        if (node == goalNode)
        {
            return;
        }

        startNode?.SetIsStart(false);
        startNode = node;

        if (startNode != null)
        {
            startNode.SetIsStart(true);
            startNode.SetIsWalkable(true);
        }
    }

    public void SetGoalNode(Node node)
    {
        if (node == startNode)
        {
            return;
        }

        if (node == goalNode)
        {
            return;
        }

        goalNode?.SetIsGoal(false);
        goalNode = node;

        if (goalNode != null)
        {
            goalNode.SetIsGoal(true);
            goalNode.SetIsWalkable(true);
        }
    }

    public void SetObstacleNode(Node node)
    {
        if (node == startNode)
        {
            return;
        }

        if (node == goalNode)
        {
            return;
        }

        node.SetIsWalkable(false);
    }

    public List<Node> StartSearch(SearchType searchType)
    {
        ResetGraph();

        switch (searchType)
        {
            case SearchType.BFS:
                return GenericSearch(new QueueFrontier());

            case SearchType.DFS:
                return GenericSearch(new StackFrontier());

            case SearchType.GBFS:
                return GenericSearch(new HeuristicList());

            case SearchType.AStar:
                return AStarSearch();
        }

        return null;
    }

    public void ResetGraph()
    {
        foreach (Node node in nodes.Values)
        {
            node.SetParent(null);
            node.SetIsExplored(false);
            node.SetIsPath(false);
            node.SetPathCost(0);
        }
    }

    List<Node> AStarSearch()
    {
        frontier = new CostList();
        visited.Clear();
        
        foreach (Node node in nodes.Values)
        {
            node.SetPathCost(int.MaxValue);
        }
        
        startNode.SetPathCost(0);
        frontier.Add(startNode);

        while (frontier.GetSize() != 0)
        {
            Node current = frontier.Take();

            if (current == goalNode)
            {
                break;
            }

            if (visited.Contains(current))
            {
                continue;
            }

            visited.Add(current);
            current.SetIsExplored(true);

            foreach (Node neighbour in GetNeighbours(current))
            {
                if (!neighbour.IsWalkable())
                {
                    continue;
                }

                int tentativeG = current.GetPathCost() + 1;

                if (tentativeG < neighbour.GetPathCost())
                {
                    neighbour.SetParent(current);
                    neighbour.SetPathCost(tentativeG);

                    if (!visited.Contains(neighbour))
                    {
                        frontier.Add(neighbour);
                    }
                }
            }
        }

        return GetPath(goalNode);
    }

    public void ClearObstacles()
    {
        foreach (Node node in nodes.Values)
        {
            node.SetIsWalkable(true);
        }
    }

    void Start()
    {
        startNode = GetNode(new Vector2Int(0, 0));
        goalNode = GetNode(new Vector2Int(8, 8));
        startNode.SetIsStart(true);
        goalNode.SetIsGoal(true);
    }

    void Awake()
    {
        CreateGraph();
    }

    List<Node> GenericSearch(IFrontier frontier)
    {
        this.frontier = frontier;

        visited.Clear();

        this.frontier.Add(startNode);
        visited.Add(startNode);

        while (this.frontier.GetSize() != 0)
        {
            Node currentNode = this.frontier.Take();

            if (goalNode == currentNode)
            {
                break;
            }

            VisitNode(currentNode);
        }

        return GetPath(goalNode);
    }

    void VisitNode(Node currentNode)
    {
        visited.Add(currentNode);
        currentNode.SetIsExplored(true);

        foreach (Node neighbour in GetNeighbours(currentNode))
        {
            if (visited.Contains(neighbour))
            {
                continue;
            }

            if (!neighbour.IsWalkable())
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
        int value = GetManhattanDistance(coordinates);
        Node newNode = new(coordinates, value);
        nodes[coordinates] = newNode;
    }
    
    int GetManhattanDistance(Vector2Int coordinates)
    {
        Vector2Int goalCoordinates = new(size - 1, size - 1);
        return (int)Vector2.Distance(coordinates, goalCoordinates);
    }
}
