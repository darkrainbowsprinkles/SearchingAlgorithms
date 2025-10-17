using System.Collections.Generic;

public class StackFrontier : IFrontier
{
    Stack<Node> stack = new();

    public int GetSize()
    {
        return stack.Count;
    }

    public void Add(Node node)
    {
        stack.Push(node);
    }

    public Node Take()
    {
        return stack.Pop();
    }
}