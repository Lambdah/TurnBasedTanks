using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class BinaryHeapTest
{

    [Test]
    public void BinaryHeapTestCase()
    {
        int row = 3;
        int column = 3;
        BinaryHeap heap = new BinaryHeap();
        heap.CreateHeap(row, column);

        Node one = createNode();
        Node two = createNode();
        Node three = createNode();
        heap.InsertHeap(one, 5);
        Assert.IsTrue(heap.CheckMin() == one);
        heap.InsertHeap(two, 3);
        Assert.IsTrue(heap.CheckMin() == two);
        heap.ChangeKey(one, 2);
        Assert.IsTrue(heap.CheckMin() == one);
        heap.InsertHeap(three, 6);
        Assert.IsTrue(heap.CheckMin() == one);
        heap.ExtractMinKey();
        Assert.IsTrue(heap.CheckMin() == two);
        heap.ExtractMinKey();
        Assert.IsTrue(heap.CheckMin() == three);
        heap.ExtractMinKey();
        Assert.IsTrue(heap.Empty());

        Node four = createNode();
        Node five = createNode();
        Node six = createNode();
        Node seven = createNode();


        heap.InsertHeap(five, 8);
        heap.InsertHeap(four, 9);
        heap.InsertHeap(three, 5);
        heap.InsertHeap(two, 3);
        heap.InsertHeap(one, 6);
        heap.InsertHeap(six, 1);
        Assert.IsTrue(heap.CheckMin() == six);
        Assert.IsTrue(heap.ExtractMinKey() == six);
        Assert.IsTrue(heap.ExtractMinKey() == two);
        Assert.IsTrue(heap.ExtractMinKey() == three);
        Assert.IsTrue(heap.ExtractMinKey() == one);
        Assert.IsTrue(heap.ExtractMinKey() == five);
        Assert.IsTrue(heap.ExtractMinKey() == four);
        Assert.IsTrue(heap.Empty());

        heap.InsertHeap(one, Mathf.Infinity);
        heap.InsertHeap(two, Mathf.Infinity);
        heap.InsertHeap(three, Mathf.Infinity);
        heap.InsertHeap(four, Mathf.Infinity);
        heap.InsertHeap(five, Mathf.Infinity);
        heap.InsertHeap(six, Mathf.Infinity);
        heap.InsertHeap(seven, Mathf.Infinity);
        heap.ChangeKey(six, 10);
        heap.ChangeKey(seven, 9);
        heap.ChangeKey(seven, 13);
        heap.ChangeKey(five, 12);
        Assert.IsTrue(heap.CheckMin() == six);
        Assert.IsTrue(heap.ExtractMinKey() == six);
        Assert.IsTrue(heap.ExtractMinKey() == five);
        Assert.IsTrue(heap.ExtractMinKey() == seven);
        heap.ChangeKey(four, 1);
        heap.ChangeKey(three, 1);
        heap.ChangeKey(two, 1);
        heap.ChangeKey(one, 1);
        Assert.IsTrue(heap.ExtractMinKey() == four);
        Assert.IsTrue(heap.ExtractMinKey() == one);
        Assert.IsTrue(heap.ExtractMinKey() == three);
        Assert.IsTrue(heap.ExtractMinKey() == two);
    }

    public Node createNode()
    {
        GameObject obj = new GameObject();
        Node node = obj.AddComponent<Node>();
        return node;
    }

    [Test]
    public void testOneElement()
    {
        int row = 1;
        int col = 1;
        BinaryHeap bh = new BinaryHeap();
        bh.CreateHeap(row, col);
        Node one = createNode();
        bh.InsertHeap(one, Mathf.Infinity);
        Assert.IsTrue(bh.CheckMin() == one);
        Assert.IsTrue(bh.CheckMin() == one);
        Assert.IsTrue(bh.ExtractMinKey() == one);

    }

    [Test]
    public void testSeveralElements()
    {
        int row = 3;
        int col = 3;
        BinaryHeap heap = new BinaryHeap();
        heap.CreateHeap(row, col);
        

        Node[] arrNode = new Node[10];
        for (int i=0; i < arrNode.Length; i++)
        {
            arrNode[i] = createNode();
        }

        for (int i = 9; i >= 0; i--)
        {
            heap.InsertHeap(arrNode[i], i);
        }

        heap.printHeap();

        for (int i = 0; i < arrNode.Length; i++)
        {
            Assert.IsTrue(heap.ExtractMinKey() == arrNode[i]);
        }
        Debug.Log("Test is finished");

    }
}
