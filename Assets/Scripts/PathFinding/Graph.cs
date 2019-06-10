using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour{

    [HideInInspector]public int row;
    [HideInInspector]public int column;
    public Node[,] graph;
    public BoardManager bm;
    private Transform boardHolder;
    
    
    public Node[,] CreateGraph()
    {
        
        row = bm.rows;
        column = bm.columns;
        graph = new Node[row, column];
        GameObject toInstaniate = bm.floorTiles;
        Vector3 origin = new Vector3(0, 0, 0);
        boardHolder = new GameObject("BoardGame").transform;
        

       

        for (int i=0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                // Node node = new Node();
                GameObject instance =
                    Instantiate(toInstaniate, origin, Quaternion.identity) as GameObject;
                Node node = instance.GetComponent<Node>();
                graph[i, j] = node;
                node.posX = j;
                node.posY = i;
                // Default cost to 1
                node.cost = 1;
                node.tile = instance;
                instance.transform.SetParent(boardHolder);
            }
        }

        SetupNeighbor(row, column);

        return graph;
    }

    
    public void ClearGraph()
    {
        graph = new Node[0,0];
    }

    protected void SetupNeighbor(int row, int column)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Node node = graph[i, j];
                node.Neighbors = new List<Node>();
                
                // Neighbor to the left
                if (j > 0)
                {
                    node.Neighbors.Add(graph[i, j-1]);
                }
                // Neighbor to the right
                if (j < column-1)
                {
                    node.Neighbors.Add(graph[i, j + 1]);
                }
                // Neighbor to the bottom
                if (i > 0)
                {
                    node.Neighbors.Add(graph[i-1, j]);
                }
                // Neighbor to the top
                if (i < row-1)
                {
                    node.Neighbors.Add(graph[i + 1, j]);
                }

            }
        }

    }

    private void Awake()
    {
        bm = GetComponent<BoardManager>();
    }

}
