using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeClick : MonoBehaviour {
    int playerTurn;
    GameObject tile;
    Node node;
    GameManager gm;
    TankManager[] tm;
    TankBoardMovement[] player;
	// Use this for initialization
	void Start () {
        tile = this.transform.parent.gameObject;
        node = tile.GetComponent<Node>();
        gm = (GameManager)GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        tm = gm.m_tanks;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    

    private void OnMouseDown()
    {

        GameObject select = node.tile.transform.GetChild(1).gameObject;
        gm.m_tanks[gm.counter].move(node);
        
    }

    
}
