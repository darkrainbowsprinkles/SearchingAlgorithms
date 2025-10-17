using System.Collections.Generic;

public class QueueFrontier : IFrontier
{
    Queue<Node> queue = new();

    public int GetSize()
    {
        return queue.Count;
    }
    
    public void Add(Node node)
    {
        queue.Enqueue(node);
    }

    public Node Take()
    {
        return queue.Dequeue();        
    }
}