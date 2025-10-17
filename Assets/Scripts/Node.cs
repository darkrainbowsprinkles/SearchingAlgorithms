using System;
using UnityEngine;

public class Node
{
    public event Action OnChange;
    Vector2Int coordinates;
    int value;
    Node parent;
    bool isWalkable = true;
    bool isExplored = false;
    bool isPath = false;

    public Node(Vector2Int coordinates, int value)
    {
        this.coordinates = coordinates;
        this.value = value;
    }

    public Vector2Int GetCoordinates()
    {
        return coordinates;
    }

    public int GetValue()
    {
        return value;
    }

    public Node GetParent()
    {
        return parent;
    }

    public bool IsWalkable()
    {
        return isWalkable;
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

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
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