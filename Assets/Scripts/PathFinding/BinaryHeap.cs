using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryHeap {
    int row;
    int column;

    int arrayCounter = 1;
    Node[] array;
    Dictionary<Node, float> heapDict;
    Dictionary<Node, int> heapLocation;

    public void printHeap()
    {
        for (int i=0; i < array.Length; i++)
        {
            Debug.Log(i + " " + array[i]);
        }
    }

    public void CreateHeap(int rows, int columns)
    {
        row = rows;
        column = columns;
        int arraySize = row * column;
        array = new Node[arraySize+10];
        heapDict = new Dictionary<Node, float>();
        heapLocation = new Dictionary<Node, int>();
        array[0] = null;
    }

    public void InsertHeap(Node node, float dist)
    {
        array[arrayCounter] = node;
        heapDict.Add(node, dist);
        heapLocation.Add(node, arrayCounter);
        // HeapifyUp(arrayCounter);
        HeapifyUp(node);
        arrayCounter += 1;
    }

    private void Swap(int first, int second)
    {
        
        Node temp = array[first];
        
        array[first] = array[second];
        array[second] = temp;
        
        heapLocation[array[first]] = first;
        heapLocation[array[second]] = second;

        // Debugging purposes
        // Node firstNode = array[first];
        // Node secondNode = array[second];


        // int firstN = heapLocation[firstNode];
        // int secondN = heapLocation[secondNode];

    }

    public float DistNode(Node node)
    {
        return heapDict[node];
    }

    public void ChangeKey(Node node, float dist)
    {
        
        heapDict[node] = dist;
        HeapifyUp(node);
        HeapifyDown(node);
    }

    public Node ExtractMinKey()
    {
        
        Node min = array[1];
        arrayCounter -= 1;
        array[1] = array[arrayCounter];
        array[arrayCounter] = null;
        heapDict.Remove(min);
        heapLocation.Remove(min);
        if (array[1] == null)
        {

        }
        else
        {
            heapLocation[array[1]] = 1;
            HeapifyDown(array[1]);
        }
        
        return min;
    }

    private int CheckLeftAndCheckRight(int pos)
    {
        // Left and right are the left and right child of the pos, respectively.
        
        int left = 2 * pos;
        int right = 2 * pos + 1;

        if (left > array.Length && right > array.Length)
        {
            
            return pos;
        }
        Node leftNode = array[left];
        Node rightNode = array[right];
        float costLeft = Mathf.Infinity;
        float costRight = Mathf.Infinity;
        // the position is a leaf
        if (leftNode == null && rightNode == null)
        {
            return pos;
        }
        // Only child on the left side
        if (rightNode == null && leftNode != null)
        {
            costLeft = heapDict[leftNode];
            costRight = Mathf.Infinity;
        }else if (rightNode != null && leftNode == null)
        {
            
            // Should not occur if it is a full binary tree
            throw new System.Exception("Left node should not be empty");
        }else if (rightNode != null && leftNode != null)
        {
            
            costLeft = heapDict[leftNode];
            costRight = heapDict[rightNode];
            
        }
        
        float posCost = heapDict[array[pos]];
        
        if (costRight > costLeft)
        {
            // Cost of left is small than cost of right
            // check if cost of left is greater than the pos
            if (costLeft < posCost)
            {
                Swap(left, pos);
                return left;

            }
            
        }
        else
        {
            // Cost of right is small than cost of left
            // Check if the cost 
            if (costRight < posCost)
            {
                Swap(right, pos);
                return right;
            }
            
        }
        return pos;
    }

    private void HeapifyDown(Node no)
    {
        int pos = heapLocation[no];
        int i = pos;
        while (i < arrayCounter)
        {
            int prevPos = i;
            // Update i, if i is not updated then the loop breaks
            i = CheckLeftAndCheckRight(i);
            if (prevPos == i)
            {
                break;
            }
            
        }
        heapLocation[no] = i;
        
    }

    private void HeapifyUp(Node node)
    {
        Node no = node;
        int pos = heapLocation[no];   
        // Check if the heap only contains one node
        if (pos <= 1)
        {
            return;
        }
        if (array[pos] != no)
        {
            Debug.Log("Suppose to be same");
        }
        //int parentPos = GetParent(pos);
        //Node no = array[pos];
        //Node noParent = array[parentPos];
        int parentPos;
        Node noParent;
        float parentDist;
        float currDist;
        while (pos > 1)
        {
            parentPos = GetParent(pos);
            noParent = array[parentPos];
            parentDist = heapDict[noParent];
            currDist = heapDict[no];
            // Parent is less than or equal to the child node
            if (parentDist <= currDist)
            {
                break;
            }
            else
            {
                //Parent is greater than the child node
                //have to swap up the child
                Swap(pos, parentPos);
                pos = parentPos;
                //parentPos = GetParent(pos);
                //no = array[pos];
                //noParent = array[parentPos];

                

            }
        }
        heapLocation[no] = pos;
        


    }

    private int GetParent(int k)
    {
        float value;
        value = k / 2;
        if (value >= 1)
        {
            return (int)Mathf.Floor(value);
        }
        return 1;
    }

    public Node CheckMin()
    {
        return array[1];
    }

    public bool Empty()
    {
        
        if (arrayCounter == 1)
        {
            return true;
        }
        return false;
    }
}

