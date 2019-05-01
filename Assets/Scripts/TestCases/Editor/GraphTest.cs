using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class GraphTest {

    Node[,] graph;
    Graph k = new Graph();

    [Test]
	public void NewEditModeTestSimplePasses() {
        // Use the Assert class to test conditions.
        int row = 5;
        int col = 5;
        Debug.Log("Testing");
        GameObject gameObj = new GameObject();
        BoardManager bm = createBoardManager(gameObj, row, col);
        Graph gra = addGraphObject(gameObj);
        gra.CreateGraph();
        graph = gra.graph;
        Assert.IsTrue(TestGraphValue(0, 0));
        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < col; y++)
            {
                List<Node> neighbors = graph[x, y].Neighbors;
                Assert.IsTrue(neighbors.Count < 5);
                if (x > 0 && x < col-1)
                {
                    Assert.IsTrue(neighbors.Contains(graph[x + 1, y]));
                    Assert.IsTrue(neighbors.Contains(graph[x - 1, y]));
                }

                if (y > 0 && y < row-1)
                {
                    Assert.IsTrue(neighbors.Contains(graph[x, y + 1]));
                    Assert.IsTrue(neighbors.Contains(graph[x, y - 1]));
                }
            }
        }



    }

    [Test]
    public void nodeNeighborsPass()
    {
        int row = 15;
        int col = 15;
        GameObject obj = new GameObject();
        createBoardManager(obj, row, col);
        Graph gra = addGraphObject(obj);
        gra.CreateGraph();
        graph = gra.graph;
        Assert.IsNotNull(graph);
        Assert.IsTrue(TestGraphValue(1,1));
        Assert.IsTrue(graph[0,0] != null);
        for (int i=0; i < row; i++)
        {
            for (int j=0; j < col; j++)
            {
                List<Node> neighbors = graph[i, j].Neighbors;
                if (i == 0 && j == 0)
                {
                    // Corner
                    Assert.IsTrue(neighbors.Count == 2);
                    Assert.IsTrue(neighbors.Contains(graph[i + 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j+1]));
                }
                else if (i == 0 && j == (col - 1))
                {
                    //Corner
                    Assert.IsTrue(neighbors.Count == 2);
                    Assert.IsTrue(neighbors.Contains(graph[i + 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j - 1]));
                }
                else if (i == (row-1) && j == 0)
                {
                    //Corner
                    Assert.IsTrue(neighbors.Count == 2);
                    Assert.IsTrue(neighbors.Contains(graph[i - 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j + 1]));
                }
                else if (i == (row-1) && j == (col-1))
                {
                    //Corner
                    Assert.IsTrue(neighbors.Count == 2);
                    Assert.IsTrue(neighbors.Contains(graph[i - 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j - 1]));
                }
                else if (i == 0 && j < (col - 1))
                {
                    // side
                   Assert.IsTrue(neighbors.Count == 3);
                    Assert.IsTrue(neighbors.Contains(graph[i + 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j + 1]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j - 1]));

                }
                else if (i < row && j == (col - 1)){
                    // side
                    Assert.IsTrue(neighbors.Count == 3);
                    Assert.IsTrue(neighbors.Contains(graph[i + 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i - 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j - 1]));
                }
                else if (i == (row - 1) && j < col)
                {
                    // side
                    Assert.IsTrue(neighbors.Count == 3);
                    Assert.IsTrue(neighbors.Contains(graph[i, j + 1]));
                    Assert.IsTrue(neighbors.Contains(graph[i - 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j - 1]));
                }
                else if (i < row && j == 0)
                {
                    // side
                    Assert.IsTrue(neighbors.Count == 3);
                    Assert.IsTrue(neighbors.Contains(graph[i + 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j + 1]));
                    Assert.IsTrue(neighbors.Contains(graph[i - 1, j]));
                }
                else if ( i < row && j < col)
                {
                    Assert.IsTrue(neighbors.Count == 4);
                    Assert.IsTrue(neighbors.Contains(graph[i + 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j + 1]));
                    Assert.IsTrue(neighbors.Contains(graph[i - 1, j]));
                    Assert.IsTrue(neighbors.Contains(graph[i, j - 1]));
                }
            }
        }
        Debug.Log("Test is finished");
    }

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
        node.AddComponent<Node>();
        bm.floorTiles = node;
        bm.rows = rows;
        bm.columns = col;
        return bm;
    }

    [Test]
    public void TestGraphConstructionPass()
    {

        int row = 6;
        int col = 6;
        GameObject gameObj = new GameObject();
        Graph gra = gameObj.AddComponent<Graph>();
        BoardManager bm = createBoardManager(gameObj, row, col);
        gra.bm = bm;
        gra.CreateGraph();
        Debug.Log("generating the graphera");
        Assert.NotNull(gra);
        Assert.NotNull(gra.graph[0, 0]);
        Assert.IsTrue(true);
    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator NewEditModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}

  
    public bool TestGraphValue(int x, int y)
    {
        if (graph[x, y] == null)
        {
            return false;
        }
        return true;
    }

}
