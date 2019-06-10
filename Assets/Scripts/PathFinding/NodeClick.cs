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
    // Refactor this so instead of using a game object use a UI element that is in world space
    // that one can click on. Doing this will hopefully prevent the error of when clicking a targetNode, it will 
    // not show the pathway is null error
    private void OnMouseDown()
    {

        GameObject select = node.tile.transform.GetChild(1).gameObject;
        player[gm.counter].GridLocationMove(node);
        player[gm.counter].targetNode = node;
        player[gm.counter].currNode = node;
        
        
    }

    
}
