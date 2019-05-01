using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch {
    Graph gra;
    Node[,] graph;
    Dictionary<Node, bool> visited;
    List<Node> traveable;
    Queue<Node> nodeCheck;
    Dictionary<Node, int> Cost;

    private void createBFS()
    {
        visited = new Dictionary<Node, bool>();
        traveable = new List<Node>();
        nodeCheck = new Queue<Node>();
        Cost = new Dictionary<Node, int>();
        for (int i=0; i < gra.row; i++)
        {
            for (int j=0; j < gra.column; j++)
            {
                visited.Add(graph[i, j], false);
            }
        }
    }

    private int queueNode(Node node, Node currNode)
    {
        if (visited[node])
        {
            // Node already visited so do nothing
            
        }
        else
        {
            nodeCheck.Enqueue(node);
            traveable.Add(node);
            visited[node] = true;
            Cost[node] = Cost[currNode] + (int) node.cost;
        }
        return Cost[node];
    }

    private Node dequeNode()
    {
        return nodeCheck.Dequeue();
    }

    public List<Node> BFS(Graph grap, Node currPos, int speed)
    {
        gra = grap;
        graph = gra.graph;
        createBFS();

        nodeCheck.Enqueue(currPos);
        visited[currPos] = true;
        Cost[currPos] = 0;

        Node nodeDeque;
        // float dist;
        for (int i=0; i <= speed; i++)
        {
            nodeDeque = dequeNode();
            // traveable.Add(nodeDeque);
            foreach(Node k in nodeDeque.Neighbors)
            {
                queueNode(k, nodeDeque);
            }
        }

        return traveable;
    }

    public List<Node> traveableNodes()
    {
        return traveable;
    }
}
