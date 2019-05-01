using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class PathFindTest {

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
        Node no = node.AddComponent<Node>();
        bm.floorTiles = node;
        bm.rows = rows;
        bm.columns = col;
        return bm;
    }

    [Test]
    public void TestGraphCreation()
    {
        int row = 5;
        int col = 5;
        GameObject obj = new GameObject();
        createBoardManager(obj, row, col);
        Graph gra = addGraphObject(obj);
        gra.CreateGraph();
        Assert.NotNull(gra.graph);

        PathFind pf = new PathFind();
        pf.CreatePaths(gra, gra.graph[0, 0]);
        
        Assert.IsTrue(pf.NodeCost(gra.graph[3, 0]) == 3.0f);
        Debug.Log("Finished Testing");
    }

    [Test]
    public void FiveSquareGraphPathFind()
    {
        int row = 5;
        int col = 5;
        GameObject obj = new GameObject();
        createBoardManager(obj, row, col);
        Graph gra = addGraphObject(obj);
        gra.CreateGraph();
        Assert.NotNull(gra.graph);

        PathFind pf = new PathFind();
        Node[,] graph = gra.graph;
        Node startingNode = graph[row-1, col-1];

        pf.CreatePaths(gra, startingNode);
        Node targetNode = graph[0,3];
        Assert.IsTrue(pf.NodeCost(targetNode) == 5.0f);
        Debug.Log("Passed testing path 1");

        Node secondNode = graph[0, 0];
        pf.CreatePaths(gra, targetNode);
        Assert.IsTrue(pf.NodeCost(secondNode) == 3.0f);
        Debug.Log("Passed testing Path 2");

        pf.CreatePaths(gra, graph[0, 0]);
        
        Assert.IsTrue(pf.NodeCost(graph[0, 4]) == 4.0f);
        Debug.Log("Passed testing path 3");

        pf.CreatePaths(gra, graph[0, 4]);
        Assert.IsTrue(pf.NodeCost(graph[4, 4]) == 4.0f);
        Debug.Log("Passed testing path 4");

        pf.CreatePaths(gra, graph[4, 4]);
        Assert.IsTrue(pf.NodeCost(graph[4, 0]) == 4.0f);
        Debug.Log("Passed Testing path 5");

        pf.CreatePaths(gra, graph[4, 0]);
        Debug.Log("Length " + pf.NodeCost(graph[4, 0]));
        Debug.Log("Length " + pf.NodeCost(graph[3, 0]));
        Debug.Log("Length " + pf.NodeCost(graph[2, 0]));
        Debug.Log("Length " + pf.NodeCost(graph[1, 0]));
        Debug.Log("Length " + pf.NodeCost(graph[0, 0]));
        Assert.IsTrue(pf.NodeCost(graph[0, 0]) == 4.0f);
        Debug.Log("Passed Testing path 6");
    }

    [Test]
    public void FiveSquareDebug()
    {
        int row = 5;
        int col = 5;
        GameObject obj = new GameObject();
        createBoardManager(obj, row, col);
        Graph gra = addGraphObject(obj);
        gra.CreateGraph();
        Assert.NotNull(gra.graph);
        Node[,] graph = gra.graph;
        PathFind pf = new PathFind();

        pf.CreatePaths(gra, graph[4, 0]);
        Debug.Log("Length (4,0)" + pf.NodeCost(graph[4, 0]));
        Debug.Log("Length (3,0)" + pf.NodeCost(graph[3, 0]));
        Debug.Log("Length (2,0)" + pf.NodeCost(graph[2, 0]));
        Debug.Log("Length (1,0)" + pf.NodeCost(graph[1, 0]));
        Debug.Log("Length (0,0)" + pf.NodeCost(graph[0, 0]));

        Debug.Log("Length (2,1)" + pf.NodeCost(graph[2, 1]));
        Debug.Log("Length (1,1)" + pf.NodeCost(graph[1, 1]));
        Assert.IsTrue(pf.NodeCost(graph[0, 0]) == 4.0f);
    }


    [Test]
	public void PathFindTestingFifteenByFifteen()
    {
        int row = 15;
        int col = 15;
        GameObject obj = new GameObject();
        createBoardManager(obj, row, col);
        Graph gra = addGraphObject(obj);
        gra.CreateGraph();
        Assert.NotNull(gra.graph);
        Node[,] graph = gra.graph;
        PathFind pf = new PathFind();

        pf.CreatePaths(gra, graph[14, 0]);
        Assert.IsTrue(pf.NodeCost(graph[0, 0]) == 14.0f);
        pf.CreatePaths(gra, graph[0, 0]);
        Assert.IsTrue(pf.NodeCost(graph[14, 1]) == 15.0f);
        pf.CreatePaths(gra, graph[0, 0]);
        Assert.IsTrue(pf.NodeCost(graph[14, 14]) == 28.0f);
        Debug.Log("Testing is finished");
    }

    [Test]
    public void PathFindTestInfinity()
    {
        int row = 15;
        int col = 15;
        GameObject obj = new GameObject();
        createBoardManager(obj, row, col);
        Graph gra = addGraphObject(obj);
        gra.CreateGraph();
        Assert.NotNull(gra.graph);
        Node[,] graph = gra.graph;
        PathFind pf = new PathFind();
        pf.CreatePaths(gra, graph[0, 0]);

        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < col; y++)
            {
                // Debug.Log("x " + x + " y " + y + " Cost:" + pf.NodeCost(graph[x, y]));
                Assert.IsFalse(pf.NodeCost(graph[x,y]) == Mathf.Infinity);
            }
        }
        Debug.Log("Testing finished");
    }

    [Test]
    public void PathFindTest30by30()
    {
        int row = 30;
        int col = 30;
        GameObject obj = new GameObject();
        createBoardManager(obj, row, col);
        Graph gra = addGraphObject(obj);
        gra.CreateGraph();
        Assert.NotNull(gra.graph);
        Node[,] graph = gra.graph;
        PathFind pf = new PathFind();
        pf.CreatePaths(gra, graph[0, 0]);


        int cost;
        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < col; y++)
            {
                
                cost = x + y;
                // Debug.Log("x " + x + " y " + y + " Cost:" + pf.NodeCost(graph[x, y]) + " other cost: " + cost);
                Assert.IsTrue(pf.NodeCost(graph[x, y]) == cost );
            }
        }
        Debug.Log("Testing finished");
    }
}
