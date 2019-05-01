using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeClick : MonoBehaviour {
    int playerTurn;
    GameObject tile;
    Node node;
    GameManager gm;
    TankBoardMovement[] player;
	// Use this for initialization
	void Start () {
        tile = this.transform.parent.gameObject;
        node = tile.GetComponent<Node>();
        gm = (GameManager)GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        player = gm.player;
        // player = (TankBoardMovement)GameObject.FindGameObjectsWithTag("Player").GetComponent<TankBoardMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        // Debug.Log(node.ToString());
        player[gm.counter].GridLocationMove(node);
        player[gm.counter].targetNode = node;
        player[gm.counter].currNode = node;
    }

    
}
