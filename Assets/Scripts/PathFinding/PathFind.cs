using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind {

    Graph g;
    Node start;
    float speed;
    public Dictionary<Node, float> costPath;
    public Dictionary<Node, Node> PrevNode;
    public Queue<Node> s;
    private BreadthFirstSearch bfs = new BreadthFirstSearch();

    public List<Node> FindTargeteableGrid(Graph graphs, Node starts, int speeds)
    {
        return bfs.BFS(graphs, starts, speeds);
    }


    public void CreatePaths(Graph graphs, Node starts)
    {
        g = graphs;
        start = starts;
        // speed = speeds;
        PathFinder();
        
    }

    private void PathFinder()
    {
        s = new Queue<Node>();
        costPath = new Dictionary<Node, float>();
        PrevNode = new Dictionary<Node, Node>();
        BinaryHeap pq = new BinaryHeap();
        float currSpeed=0;
        pq.CreateHeap(g.row, g.column);
        for (int i=0; i < g.row; i++)
        {
            for (int j = 0; j < g.column; j++)
            {
                Node k = g.graph[i, j];
                PrevNode[k] = null;
                costPath[k] = Mathf.Infinity;
                pq.InsertHeap(g.graph[i,j], Mathf.Infinity);
            }
        }
        // Sets the initial starting node
        costPath[start] = 0;
        pq.ChangeKey(start, 0);

        
        while(!pq.Empty())
        {
            Node u = pq.ExtractMinKey();
            
            
            currSpeed = costPath[u];
            if (currSpeed == Mathf.Infinity)
            {
                Debug.Log("Error cannot have infinity " + u.ToString());
            }
            s.Enqueue(u);
            
            
            foreach (Node v in u.Neighbors)
            {
                
                if (s.Contains(v))
                {
                    // Pass
                    // v is already in the shortest path of S
                }
                else
                {
                    // Updates the shortest Path of nodes not in the set S
                    // which contains the shortest Paths
                    float alternate = costPath[u] + v.cost;
                    if (alternate < costPath[v])
                    {
                        pq.ChangeKey(v, alternate);
                        costPath[v] = alternate;
                        PrevNode[v] = u;
                    }

                }

            }
        }
    }

    public float NodeCost(Node node)
    {
        return costPath[node];
    }

    public Node Prev(Node node)
    {
        return PrevNode[node];
    }

    public List<Node> PathWay(Node target)
    {
        
        
        List<Node> path = new List<Node>();
        path.Add(target);
        while (target !=start)
        {
            Node prev = PrevNode[target];
            

            if (prev == null)
            {
                return path;
            }
            else
            {
                path.Add(prev);
                target = prev;
            }
        }

        path.Reverse();
        
        return path;

    }
}
