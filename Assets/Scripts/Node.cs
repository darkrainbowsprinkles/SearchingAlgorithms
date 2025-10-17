using System;
using UnityEngine;

public class Node
{
    public event Action OnChange;
    Vector2Int coordinates;
    int value;
    Node parent;
    bool isStart = false;
    bool isGoal = false;
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

    public bool IsStart()
    {
        return isStart;
    }

    public bool IsGoal()
    {
        return isGoal;
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
        OnChange?.Invoke();
    }

    public void SetIsStart(bool isStart)
    {
        this.isStart = isStart;
        OnChange?.Invoke();
    }

    public void SetIsGoal(bool isGoal)
    {
        this.isGoal = isGoal;
        OnChange?.Invoke();
    }

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
        OnChange?.Invoke();
    }

    public void SetIsExplored(bool isExplored)
    {
        this.isExplored = isExplored;
        OnChange?.Invoke();
    }

    public void SetIsPath(bool isPath)
    {
        this.isPath = isPath;
        OnChange?.Invoke();
    }
}