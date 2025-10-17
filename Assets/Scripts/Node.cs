using System;
using UnityEngine;

public class Node
{
    public event Action OnChange;
    Vector2Int coordinates;
    Node parent;
    bool isExplored;
    bool isPath;

    public Node(Vector2Int coordinates)
    {
        this.coordinates = coordinates;
    }

    public Vector2Int GetCoordinates()
    {
        return coordinates;
    }

    public Node GetParent()
    {
        return parent;
    }

    public bool IsExplored()
    {
        return isExplored;
    }

    public bool IsPath()
    {
        return isPath;
    }

    public void SetParent(Node parent)
    {
        this.parent = parent;
        OnChange.Invoke();
    }

    public void SetIsExplored(bool isExplored)
    {
        this.isExplored = isExplored;
        OnChange.Invoke();
    }

    public void SetIsPath(bool isPath)
    {
        this.isPath = isPath;
        OnChange.Invoke();
    }
}