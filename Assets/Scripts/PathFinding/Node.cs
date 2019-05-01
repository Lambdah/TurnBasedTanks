using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node: MonoBehaviour{
    public GameObject tile;
    public int posX;
    public int posY;

    public float cost;

    [HideInInspector]public List<Node> Neighbors;
    public GameObject gameManager;
    
    public override string ToString()
    {
        return "X" + posX + " Y " + posY + " cost " + cost;
    }

    public Vector3 TileWorldPosition()
    {
        return new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
    }

    
}
