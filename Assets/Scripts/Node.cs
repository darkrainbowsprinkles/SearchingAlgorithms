using System;
using UnityEngine;

public class Node
{
    public event Action OnChange;
    Vector2Int coordinates;
    int heuristicValue;
    int pathCost;
    Node parent;
    bool isStart = false;
    bool isGoal = false;
    bool isWalkable = true;
    bool isExplored = false;
    bool isPath = false;

    public Node(Vector2Int coordinates, int heuristicValue)
    {
        this.coordinates = coordinates;
        this.heuristicValue = heuristicValue;
    }

    public Vector2Int GetCoordinates()
    {
        return coordinates;
    }

    public int GetHeuristicValue()
    {
        return heuristicValue;
    }

    public int GetPathCost()
    {
        return pathCost;
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

    public void SetPathCost(int pathCost)
    {
        this.pathCost = pathCost;
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