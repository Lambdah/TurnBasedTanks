using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class BFSTest {

    public Graph addGraphObject(GameObject obj)
    {
        Graph gra = obj.AddComponent<Graph>();
        gra.bm = obj.GetComponent<BoardManager>();
        return gra;
    }

    public BoardManager createBoardManager(GameObject obj, int rows, int col)
    {
        BoardManager bm = obj.AddComponent<BoardManager>();
        GameObject node = new GameObject();
        bm.floorTiles = node;
        bm.rows = rows;
        bm.columns = col;
        return bm;
    }

    [Test]
    public void BFSinitialTest()
    {
        int row = 5;
        int col = 5;
        int speed = 2;
        GameObject obj = new GameObject();
        createBoardManager(obj, row, col);
        Graph gra = addGraphObject(obj);
        gra.CreateGraph();
        Node[,] graph = gra.graph;
        Assert.NotNull(gra.graph);
        BreadthFirstSearch bfs = new BreadthFirstSearch();
        bfs.BFS(gra, graph[0,0], speed);
        List<Node> reachableNodes = bfs.traveableNodes();
        int reach;
        
       
        for (int i = 0; i < reachableNodes.Count; i++)
        {
            reach = reachableNodes[i].posX + reachableNodes[i].posY;
            Assert.IsTrue(reach <= speed);
        }

        Debug.Log("Testing is finished");
    }

    public int reachability(Node node, Node start)
    {
        return node.posX + node.posY - start.posX - start.posY;
    }

    [Test]
    public void BFSMediumTest()
    {
        int row = 70;
        int col = 70;
        int speed = 5;
        GameObject obj = new GameObject();
        createBoardManager(obj, row, col);
        Graph gra = addGraphObject(obj);
        gra.CreateGraph();
        Node[,] graph = gra.graph;
        Assert.NotNull(gra.graph);
        BreadthFirstSearch bfs = new BreadthFirstSearch();
        bfs.BFS(gra, graph[0, 0], speed);
        List<Node> reachableNodes = bfs.traveableNodes();
        int reach;
        
        for (int i=0; i < reachableNodes.Count; i++)
        {
            reach = reachableNodes[i].posX + reachableNodes[i].posY;
            Assert.IsTrue(reach <= speed);
        }

        speed = 20;
        bfs.BFS(gra, graph[50, 50], speed);
        reachableNodes = bfs.traveableNodes();
        for (int i = 0; i < reachableNodes.Count; i++)
        {
            // reach = reachableNodes[i].posX + reachableNodes[i].posY - 100;
            reach = reachability(reachableNodes[i], graph[50, 50]);
            Assert.IsTrue(reach <= speed);
        }

        speed = 10;

        for (int i=0; i < row; i++)
        {
            for (int j=0; j < col; j++)
            {
                bfs.BFS(gra, graph[i,j], speed);
                reachableNodes = bfs.traveableNodes();
                for (int k=0; k < reachableNodes.Count; k++)
                {
                    reach = reachability(reachableNodes[k], graph[i, j]);
                    Assert.IsTrue(reach <= speed);
                }
            }
        }

        Debug.Log("Testing complete");
    }

    
}
